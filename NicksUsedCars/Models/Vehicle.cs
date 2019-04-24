using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class Vehicle
    {
        // Identifying what vehicle it is
        /// <summary>
        /// Name of manufacturer of vehicle
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Name of vahicle itself
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Name of specific type of model if applicable
        /// ex: Camry SE vs Camry XSE (only the SE or XSE part)
        /// </summary>
        public string ModelType { get; set; }
        /// <summary>
        /// year that vehicle was manufactured
        /// </summary>
        public int ManufacturedYear { get; set; }

        // Specifications of vehicle
        /// <summary>
        /// Name of style the shape of the vehicle is
        /// </summary>
        public string BodyStyle { get; set; }
        /// <summary>
        /// Object that holds information about engine specifications
        /// </summary>
        public EngineType Engine { get; set; }
        /// <summary>
        /// Type of transmission used in vehicle
        /// </summary>
        public string TransmissionType { get; set; }
        /// <summary>
        /// Which set of wheels gets the power from the engine
        /// </summary>
        public string DriveType { get; set; }

        // Other info on vehicle
        /// <summary>
        /// General color of outside of vehicle
        /// </summary>
        public string ExteriorColor { get; set; }
        /// <summary>
        /// General color of inside of car
        /// </summary>
        public string  InteriorColor { get; set; }
        /// <summary>
        /// How many miles vehicle has been driven
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// Unique number given to vehicle to find in system
        /// </summary>
        public int StockNumber { get; set; }
        /// <summary>
        /// Vehicle Identification Number for vehicle
        /// </summary>
        public string Vin { get; set; }

        public string GetVehicleName()
        {
            return $"{ManufacturedYear} {Manufacturer} {Model} {ModelType}";
        }
    }

    public class EngineType
    {
        /// <summary>
        /// fuel type use by vehicle. Gas, diesel, electric, etc.
        /// </summary>
        public string FuelType { get; set; }
        /// <summary>
        /// shortened term of cylinder ayout and number of cylinders.
        /// ex: inline 4 cylinder is I4, boxer 6 cylinder is H6, etc
        /// </summary>
        public string EngineConfiguration { get; set; }
        /// <summary>
        /// size of engine in liters, followed by "L" to indicate size is in liters
        /// </summary>
        public string EngineSize { get; set; }
        /// <summary>
        /// amount of power engine output is measured in horsepower
        /// </summary>
        public int Horsepower { get; set; }

        public override string ToString()
        {
            return $"{FuelType} {EngineConfiguration} {EngineSize}/{Horsepower}";
        }
    }
}
