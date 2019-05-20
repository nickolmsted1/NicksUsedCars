using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public static class VehicleDb
    {
        public static Vehicle Add(Vehicle v, NicksUsedCarsContext context)
        {
            context.Vehicles.Add(v);
            context.SaveChanges();
            return v;
        }

        public static List<string> GetManufacturerNames (NicksUsedCarsContext context)
        {
            return context.Vehicles.Select(p => p.Manufacturer).ToList();
        }

        public static List<string> GetModelNames (NicksUsedCarsContext context)
        {
            return context.Vehicles.Select(p => p.Model).ToList();
        }

        public static List<Vehicle> SearchVehicle(NicksUsedCarsContext context, SearchCriteria search)
        {
            IQueryable<Vehicle> vehicles = context.Vehicles.Select(p => p);

            if (search.Manufacturer != null)
            {
                vehicles = context.Vehicles.Where(p => p.Manufacturer == search.Manufacturer);
            }
            if (search.Model != null)
            {
                vehicles = context.Vehicles.Where(p => p.Model == search.Model);
            }
            if (search.ModelType != null)
            {
                vehicles = context.Vehicles.Where(p => p.ModelType.Contains(search.ModelType));
            }
            if (search.MinYear.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.ManufacturedYear >= search.MinYear);
            }
            if (search.MaxYear.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.ManufacturedYear <= search.MaxYear);
            }
            if (search.BodyStyle != null)
            {
                vehicles = context.Vehicles.Where(p => p.BodyStyle == search.BodyStyle);
            }
            if (search.TransmissionType != null)
            {
                if (Enum.IsDefined(typeof(Transmission), search.TransmissionType))
                {
                    Enum.TryParse(search.TransmissionType, out Transmission transmission);
                
                    vehicles = context.Vehicles.Where(p => p.TransmissionType == transmission);
                }
            }
            if (search.DriveType != null)
            {
                vehicles = context.Vehicles.Where(p => p.DriveType == search.DriveType);
            }
            if (search.ExteriorColor != null)
            {
                vehicles = context.Vehicles.Where(p => p.ExteriorColor.Contains(search.ExteriorColor));
            }
            if (search.InteriorColor != null)
            {
                vehicles = context.Vehicles.Where(p => p.InteriorColor.Contains(search.InteriorColor));
            }
            if (search.MinMileage.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.Mileage >= search.MinMileage
);
            }
            if (search.MaxMileage.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.Mileage <= search.MaxMileage);
            }
            if (search.EngineType != null)
            {
                vehicles = context.Vehicles.Where(p => p.EngineConfiguration == search.EngineType);
            }
            if (search.FuelType != null)
            {
                vehicles = context.Vehicles.Where(p => p.FuelType == search.FuelType);
            }
            if (search.LowPrice.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.Price >= search.LowPrice
);
            }
            if (search.HighPrice.HasValue)
            {
                vehicles = context.Vehicles.Where(p => p.Price <= search.HighPrice);
            }

            return vehicles.ToList();
        }
    }
}
