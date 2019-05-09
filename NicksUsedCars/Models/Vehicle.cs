using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public enum Drive { AWD, RWD, FWD }

    public enum Fuel { Gas, Diesel, Electric, Hybrid }

    public enum Transmission { Manual, Automatic, CVT, Sequential}

    public enum Engine { I3, I4, I5, I6, V4, V6, V8, V10, V12, V16, VR4, VR6, W8, W12, W16, H4, H6, H12}

    public class Vehicle
    {
        // Identifying what vehicle it is
        [Key]
        /// <summary>
        /// Unique number given to vehicle to find in system
        /// </summary>
        public int StockNumber { get; set; }

        [StringLength(maximumLength: 17, MinimumLength = 15)]
        [Display(Name = "VIN Number")]
        /// <summary>
        /// Vehicle Identification Number for vehicle
        /// </summary>
        public string Vin { get; set; }

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

        [Display(Name = "Model Type")]
        /// <summary>
        /// Name of specific type of model if applicable
        /// ex: Camry SE vs Camry XSE (only the SE or XSE part)
        /// </summary>
        public string ModelType { get; set; }

        [Required(ErrorMessage = "Please enter the manufactured year of the vehicle.")]
        [RangeUntilCurrentYear(1880, ErrorMessage = "Please enter a year between 1880 and next year")]
        [Display(Name = "Manufactured Year")]
        /// <summary>
        /// year that vehicle was manufactured
        /// </summary>
        public int ManufacturedYear { get; set; }

        // Specifications of vehicle
        [StringLength(maximumLength: 50)]
        [Display(Name = "Body Style")]
        /// <summary>
        /// Name of style the shape of the vehicle is
        /// </summary>
        public string BodyStyle { get; set; }

        [StringLength(maximumLength: 25)]
        [Display(Name = "Transmission Type")]
        /// <summary>
        /// Type of transmission used in vehicle
        /// </summary>
        public Transmission TransmissionType { get; set; }

        [StringLength(maximumLength: 3)]
        [Display(Name = "Drive Type")]
        /// <summary>
        /// Which set of wheels gets the power from the engine
        /// </summary>
        public Drive DriveType { get; set; }

        // Other info on vehicle
        [StringLength(maximumLength: 30)]
        [Display(Name = "Exterior Color")]
        /// <summary>
        /// General color of outside of vehicle
        /// </summary>
        public string ExteriorColor { get; set; }

        [StringLength(maximumLength: 30)]
        [Display(Name = "Interior Color")]
        /// <summary>
        /// General color of inside of car
        /// </summary>
        public string InteriorColor { get; set; }

        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Please enter a number above zero")]
        /// <summary>
        /// How many miles vehicle has been driven
        /// </summary>
        public int Mileage { get; set; }

        [DataType(DataType.Currency)]
        [Range(500, int.MaxValue)]
        [Required]
        /// <summary>
        /// Price of vehicle
        /// </summary>
        public int Price { get; set; }

        //Engine Information
        [Display(Name = "Fuel Type")]
        /// <summary>
        /// fuel type use by vehicle. Gas, diesel, electric, etc.
        /// </summary>
        public Fuel FuelType { get; set; }

        [StringLength(maximumLength: 5)]
        [Display(Name = "Engine Configuration")]
        /// <summary>
        /// shortened term of cylinder ayout and number of cylinders.
        /// ex: inline 4 cylinder is I4, boxer 6 cylinder is H6, etc
        /// </summary>
        public Engine EngineConfiguration { get; set; }

        [Range(0.0, 12)]
        [Display(Name = "Engine Size")]
        /// <summary>
        /// size of engine in liters, followed by "L" to indicate size is in liters
        /// </summary>
        public double EngineSize { get; set; }

        [Range(1, int.MaxValue)]
        /// <summary>
        /// amount of power engine output is measured in horsepower
        /// </summary>
        public int Horsepower { get; set; }

        public string EngineToString()
        {
            return $"{FuelType} {EngineConfiguration} {EngineSize}L/{Horsepower}";
        }


        public string GetVehicleName()
        {
            return $"{ManufacturedYear} {Manufacturer} {Model} {ModelType}";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeUntilCurrentYearAttribute : RangeAttribute
    {
        public new int Maximum = DateTime.Now.Year + 1;
        public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.Now.Year + 1)
        {

        }
    }
}
