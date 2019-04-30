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

        public int Year { get; set; }

        public string BodyStyle { get; set; }

        public string Transmission { get; set; }

        enum DriveType
        {
            AWD,
            RWD,
            FWD
        }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public int Mileage { get; set; }

        public Enum FuelType { get; set; }
    }
}
