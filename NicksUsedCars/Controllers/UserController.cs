using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NicksUsedCars.Models;

namespace NicksUsedCars.Controllers
{
    public class UserController : Controller
    {
        private readonly NicksUsedCarsContext _Context;
        private readonly IHostingEnvironment _Env;

        public UserController(NicksUsedCarsContext dbContext, IHostingEnvironment env)
        {
            _Context = dbContext;
            _Env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult SetRoles()
        {
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
            searchParams.UserSearchResults = IdentityExtension.SearchUsers(_Context, searchParams);
            return View(searchParams);
        }

        public IActionResult ChangeUserRole(string id)
        {
            var user = IdentityExtension.GetUser(_Context, id);
            if (user != null) {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = id,
                    FullName = user.FirstName + user.LastName
                };
                return View(userRoleViewModel);
            }
            else
            {
                ViewBag.NullUser = "User not found.";
                return RedirectToAction("SetRoles");
            }
            
        }
    }
}