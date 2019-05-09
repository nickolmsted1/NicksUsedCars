using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class SearchCriteria
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string ModelType { get; set; }

        public int? MinYear { get; set; }

        public int? MaxYear { get; set; }

        public string BodyStyle { get; set; }

        public Models.Transmission? TransmissionType { get; set; }

        public Models.Drive? DriveType { get; set; }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public int? MinMileage { get; set; }

        public int? MaxMileage { get; set; }

        public Models.Engine? EngineType { get; set; }

        public Models.Fuel? FuelType { get; set; }

        public int? HighPrice { get; set; }

        public int? LowPrice { get; set; }

        public List<Vehicle> SearchResults { get; set; }
    }
}
