﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NicksUsedCars.Models;

namespace NicksUsedCars.Controllers
{
    public class UserController : Controller
    {
        private readonly NicksUsedCarsContext _Context;
        private readonly IHostingEnvironment _Env;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public UserController(NicksUsedCarsContext dbContext, IHostingEnvironment env, UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _Context = dbContext;
            _Env = env;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult SetRoles()
        {
            // sets up blank search result that will be used
            UserSearch search = new UserSearch
            {
                UserSearchResults = new List<ApplicationUser>()
            };
            return View(search);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult SetRoles(UserSearch searchParams)
        {
            // uses search parameters to find user to change role of
            searchParams.UserSearchResults = IdentityExtension.SearchUsers(_Context, searchParams);
            return View(searchParams);
        }

        public async Task<IActionResult> ChangeUserRole(string id)
        {
            // uses id to get user from db
            var user = IdentityExtension.GetUser(_Context, id);
            // only goes through if user exists in db
            if (user != null) {
                // gets all roles user is in (should only be 1)
                var roles = await IdentityExtension.GetRoles(_UserManager, user);
                // sets new object to make role change easier
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = id,
                    FullName = user.FirstName + user.LastName,
                    OldRoles = roles
                };
                return View(userRoleViewModel);
            }
            // if user was not found in db
            else
            {
                ViewBag.NullUser = "User not found.";
                return RedirectToAction("SetRoles");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(UserRoleViewModel newUserRole)
        {
            
            if (ModelState.IsValid)
            {
                // role that user will be changed to
                IdentityRole role = await _RoleManager.FindByNameAsync(newUserRole.RoleName);
                
                if (role == null)
                {
                    ViewBag.RoleError = $"Role with name {newUserRole.RoleName} could not be found.";
                    return View(newUserRole.UserId);
                }
                
                // user that will have role changed
                var user = await _UserManager.FindByIdAsync(newUserRole.UserId);

                // set values for newUserRole
                newUserRole.FullName = user.FirstName + " " + user.LastName;
                newUserRole.OldRoles = await IdentityExtension.GetRoles(_UserManager, user);

                // Id of role that user will change to
                string roleId = await _RoleManager.GetRoleIdAsync(role);
                
                //keeps track if removal of old roles is successful
                IdentityResult removeRoleResult = null;
                // keeps track if adding new role is successful
                IdentityResult newRoleResult = null;

                if (newUserRole.OldRoles != null)
                {
                    // number of roles user is in (should only be 1)
                    int roles = newUserRole.OldRoles.Count;
                    // goes through all roles user is currently in and removes them 
                    // (should only be 1. this will make sure there will only be one when role is updated)
                    for (int i = 0; i < roles; i++)
                    {
                        removeRoleResult = await _UserManager.RemoveFromRoleAsync(user, newUserRole.OldRoles[i]);
                    }

                    
                    newRoleResult = await _UserManager.AddToRoleAsync(user, newUserRole.RoleName);
                }
                else
                {
                    // only goes through here if user has no roles set
                    newRoleResult = await _UserManager.AddToRoleAsync(user, newUserRole.RoleName);
                    removeRoleResult = newRoleResult;
                }

                

                // return success message to page
                if (removeRoleResult.Succeeded && newRoleResult.Succeeded && await _UserManager.IsInRoleAsync(user, newUserRole.RoleName))
                {
                    ViewBag.RoleChangeSuccess = $"{newUserRole.FullName}'s new role is {newUserRole.RoleName}.";
                    return View(newUserRole);
                }
                // returns error message to page
                else if (!(await _UserManager.IsInRoleAsync(user, newUserRole.RoleName)))
                {
                    ViewBag.RoleChangeFail = $"{newUserRole.FullName} failed to get into {newUserRole.RoleName} role.";
                    return View(newUserRole);
                }
                // returns error message to page
                else
                {
                    ViewBag.RoleChangeError = $"There were issues with updating role for {newUserRole.FullName}";
                    return View(newUserRole);
                }

            }
            // return errors if invalid
            return View(newUserRole);
        }
    }
}