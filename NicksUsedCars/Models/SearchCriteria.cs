using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class SearchCriteria
    {
        [StringLength(30)]
        public string Manufacturer { get; set; }

        [StringLength(30)]
        public string Model { get; set; }

        [StringLength(30)]
        public string ModelType { get; set; }
        
        [RangeUntilCurrentYear(1880, ErrorMessage = "Please enter a valid year")]
        public int? MinYear { get; set; }

        [RangeUntilCurrentYear(1880, ErrorMessage = "Please enter a valid year")]
        public int? MaxYear { get; set; }

        [StringLength(30)]
        public string BodyStyle { get; set; }

        public Models.Transmission? TransmissionType { get; set; }

        public Models.Drive? DriveType { get; set; }

        [StringLength(30)]
        public string ExteriorColor { get; set; }

        [StringLength(30)]
        public string InteriorColor { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinMileage { get; set; }

        [Range(0, int.MaxValue)]
        public int? MaxMileage { get; set; }

        public Models.Engine? EngineType { get; set; }

        public Models.Fuel? FuelType { get; set; }

        [Range(0, int.MaxValue)]
        public int? HighPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int? LowPrice { get; set; }

        public List<Vehicle> SearchResults { get; set; }
    }
}
