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
            List<Vehicle> vehicleList = VehicleDb.GetVehicleList(Context);
            return View(vehicleList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle v)
        {
            if (ModelState.IsValid)
            {
                // add vehicle to database
                VehicleDb.Add(v, Context);

                // add message to show user vehicle was added successfully
                ViewData["SuccessMessage"] = $"{v.ManufacturedYear} {v.Manufacturer} {v.Model} was added to database.";

                // return to page to enter more vehicles
                return View();
            }
            // reset Success Message
            ViewData["SuccessMessage"] = null;
            // return to page with errors
            return View(v);
        }

        public IActionResult Search(SearchCriteria search)
        {
            search.SearchResults = VehicleDb.SearchVehicle(Context, search);
            return View(search);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}