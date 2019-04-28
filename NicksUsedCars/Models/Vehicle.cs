using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class Vehicle
    {
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class RangeUntilCurrentYearAttribute : RangeAttribute
        {
            public new int Maximum = DateTime.Now.Year + 1;
            public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.Now.Year + 1)
            {
                
            }
        }
        // Identifying what vehicle it is
        [Required(ErrorMessage = "Please Enter a name for the manufacturer.")]
        [StringLength(maximumLength: 50)]
        /// <summary>
        /// Name of manufacturer of vehicle
        /// </summary>
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Please enter a name for the model.")]
        [StringLength(maximumLength: 50)]
        /// <summary>
        /// Name of vahicle itself
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Name of specific type of model if applicable
        /// ex: Camry SE vs Camry XSE (only the SE or XSE part)
        /// </summary>
        public string ModelType { get; set; }
        [Required(ErrorMessage = "Please enter the manufactured year of the vehicle.")]
        [RangeUntilCurrentYear(1880, ErrorMessage = "Please enter a year between 1880 and next year")]
        /// <summary>
        /// year that vehicle was manufactured
        /// </summary>
        public int ManufacturedYear { get; set; }

        // Specifications of vehicle
        [StringLength(maximumLength: 50)]
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
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Please enter a number above zero")]
        /// <summary>
        /// How many miles vehicle has been driven
        /// </summary>
        public int Mileage { get; set; }
        [Key]
        /// <summary>
        /// Unique number given to vehicle to find in system
        /// </summary>
        public int StockNumber { get; set; }
        [StringLength(maximumLength: 17)]
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
        [StringLength(maximumLength: 15)]
        /// <summary>
        /// fuel type use by vehicle. Gas, diesel, electric, etc.
        /// </summary>
        public string FuelType { get; set; }
        [StringLength(maximumLength: 5)]
        /// <summary>
        /// shortened term of cylinder ayout and number of cylinders.
        /// ex: inline 4 cylinder is I4, boxer 6 cylinder is H6, etc
        /// </summary>
        public string EngineConfiguration { get; set; }
        [StringLength(maximumLength: 5)]
        /// <summary>
        /// size of engine in liters, followed by "L" to indicate size is in liters
        /// </summary>
        public string EngineSize { get; set; }
        [Range(1, int.MaxValue)]
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
