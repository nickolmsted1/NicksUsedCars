using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NicksUsedCars.Models;

namespace NicksUsedCars.Controllers
{
    public class CarController : Controller
    {
        private readonly NicksUsedCarsContext _Context;
        private readonly IHostingEnvironment _Env;

        public CarController(NicksUsedCarsContext dbContext, IHostingEnvironment env)
        {
            _Context = dbContext;
            _Env = env;
        }

        public IActionResult Index()
        {
            // get list of vehicles from database
            List<Vehicle> vehicleList = VehicleDb.GetVehicleList(_Context);
            // pass list of vehicles into view
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
                VehicleDb.Add(v, _Context);

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
            search.SearchResults = VehicleDb.SearchVehicle(_Context, search);
            return View(search);
        }

        public IActionResult Edit(Vehicle v)
        {
            return View();
        }

        public IActionResult AddPhotos() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPhotos(Vehicle v)
        {
            ModelState.Remove(nameof(Vehicle.PhotoUrl));
            VehicleHelper.AddPhoto(v, _Env, _Context);
            return View();
        }
    }
}