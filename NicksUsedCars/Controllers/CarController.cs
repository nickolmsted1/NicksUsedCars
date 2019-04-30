using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NicksUsedCars.Models;

namespace NicksUsedCars.Controllers
{
    public class CarController : Controller
    {
        private readonly NicksUsedCarsContext Context;

        public CarController(NicksUsedCarsContext dbContext)
        {
            Context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}