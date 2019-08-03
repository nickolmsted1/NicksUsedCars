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

        [Authorize(Roles = "Admin")]
        public IActionResult SetRoles()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SetRoles(UserSearch searchParams)
        {
            List<ApplicationUser> users = IdentityExtension.SearchUsers(_Context, searchParams);
            return View(users);
        }
    }
}