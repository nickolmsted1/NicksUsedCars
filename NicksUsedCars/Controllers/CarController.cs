using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NicksUsedCars.Models;

namespace NicksUsedCars.Controllers
{
    public class CarController : Controller
    {
        private readonly NicksUsedCarsContext _Context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IHostingEnvironment _Env;

        public CarController(NicksUsedCarsContext dbContext,UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _Context = dbContext;
            _UserManager = userManager;
            _Env = env;
        }

        public IActionResult Index()
        {
            // get list of vehicles from database
            List<Vehicle> vehicleList = VehicleDb.GetVehicleList(_Context);
            // pass list of vehicles into view
            return View(vehicleList);
        }

        public async Task<IActionResult> Create()
        {
            if (await IdentityExtension.IsManagerOrAbove(_UserManager, await _UserManager.GetUserAsync(User)))
                return View();
            return RedirectToAction("Index");
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

        public async Task<IActionResult> Edit(int id)
        {
            if (await IdentityExtension.IsManagerOrAbove(_UserManager, await _UserManager.GetUserAsync(User)))
            {
                Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);

                return View(v);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Vehicle v)
        {
            if (v.Photo != null)
            {
                ModelState.Remove(nameof(Vehicle.PhotoUrl));
                VehicleHelper.AddPhoto(v, _Env, _Context);
            }

            VehicleDb.Edit(v, _Context);
            if (v != null)
            {
                ViewData["UpdateMessage"] = v.GetVehicleName() + " has been updated.";
            }
            else
            {
                ViewData["UpdateMessage"] = "Update failed.";
            }
            return View(v);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await IdentityExtension.IsManagerOrAbove(_UserManager, await _UserManager.GetUserAsync(User)))
            {
                Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);
                return View(v);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);
            bool isDeleted = VehicleDb.Delete(v, _Context);
            if (isDeleted)
                ViewBag.DeleteMessage = "Vehicle was successfully deleted.";
            else
                ViewBag.DeleteMessage = "There was an issue with deleting this vehicle.";
            return View(v);
        }

        public async Task<IActionResult> AddPhotos(int id) 
        {
            if (await IdentityExtension.IsManagerOrAbove(_UserManager, await _UserManager.GetUserAsync(User)))
            {
                Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);

                return View(v);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddPhotos(Vehicle v)
        {
            ModelState.Remove(nameof(Vehicle.PhotoUrl));
            VehicleHelper.AddPhoto(v, _Env, _Context);
            return View();
        }

        public IActionResult SelectedVehicle(int id)
        {
            Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);
            return View(v);
        }

        public async Task<IActionResult> ViewVehicleWaitList()
        {
            if (await IdentityExtension.IsEmployeeOrAbove(_UserManager, await _UserManager.GetUserAsync(User)))
            {
                List<Vehicle> vehicles = VehicleDb.GetVehicleList(_Context);
                return View(vehicles);
            }
            return RedirectToAction("SelectedVehicle");
        }

        public IActionResult MaintainWaitList(int id)
        {
            List<VehicleWaitList> waitList = VehicleDb.GetWaitListForVehicle(_Context, id);
            return View(waitList);
        }

        public IActionResult DeleteUserFromWaitList(string id, int vehicleId)
        {
            var user = new VehicleWaitList() {
                UserId = id,
                VehicleId = vehicleId
            };
            _Context.VehicleWaitList.Remove(user);
            _Context.SaveChanges();
            return RedirectToAction("MaintainWaitList");
        }

        public async Task<IActionResult> CustomerInterestedInVehicle(int id)
        {
            if (await IdentityExtension.IsCustomer(_UserManager, await _UserManager.GetUserAsync(User)))
            {
                Vehicle v = VehicleDb.GetSingleVehicle(id, _Context);
                ApplicationUser user = await _UserManager.GetUserAsync(User);
                VehicleDb.SaveToWaitList(_Context, user.Id, id);
                int waitListLength = VehicleDb.NumUsersInVehicleWaitList(_Context, id);
                string name = v.GetVehicleName();
                ViewBag.AddedToVehicleList = $"Thank you for choosing {name}.\nYou are number {waitListLength} in the queue for this vehicle.";
                return View(v);
            }
            return RedirectToAction("SelectedVehicle", new { id });
        }
    }
}