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

        public static Vehicle Edit(Vehicle v, NicksUsedCarsContext context)
        {
            context.Update(v);
            context.SaveChanges();
            return v;
        }

        public static Vehicle GetSingleVehicle(int id, NicksUsedCarsContext context)
        {
            Vehicle v = context.Vehicles.Where(p => p.StockNumber == id).Single();
            return v;
        }

        public static List<Vehicle> GetVehicleList(NicksUsedCarsContext context)
        {
            List<Vehicle> vehicleList = context.Vehicles.ToList();
            return vehicleList;
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
                //vehicles = context.Vehicles.Where(p => p.Manufacturer == search.Manufacturer);
                vehicles = from v in vehicles
                           where search.Manufacturer == v.Manufacturer
                           select v;
                            
            }
            if (search.Model != null)
            {
                //vehicles = context.Vehicles.Where(p => p.Model == search.Model);
                vehicles = from v in vehicles
                           where search.Model == v.Model
                           select v;
            }
            if (search.ModelType != null)
            {
                //vehicles = context.Vehicles.Where(p => p.ModelType.Contains(search.ModelType));
                vehicles = from v in vehicles
                           where search.ModelType == v.ModelType
                           select v;
            }
            if (search.MinYear.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.ManufacturedYear >= search.MinYear);
                vehicles = from v in vehicles
                           where v.ManufacturedYear >= search.MinYear
                           select v;
            }
            if (search.MaxYear.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.ManufacturedYear <= search.MaxYear);
                vehicles = from v in vehicles
                           where v.ManufacturedYear <= search.MaxYear
                           select v;
            }
            if (search.BodyStyle != null)
            {
                //vehicles = context.Vehicles.Where(p => p.BodyStyle == search.BodyStyle);
                vehicles = from v in vehicles
                           where search.BodyStyle == v.BodyStyle
                           select v;
            }
            if (search.TransmissionType != null)
            {
                //vehicles = context.Vehicles.Where(p => p.TransmissionType == search.TransmissionType);
                vehicles = from v in vehicles
                           where search.TransmissionType == v.TransmissionType
                           select v;
            }
            if (search.DriveType != null)
            {
                //vehicles = context.Vehicles.Where(p => p.DriveType == search.DriveType);
                vehicles = from v in vehicles
                           where search.DriveType == v.DriveType
                           select v;
            }
            if (search.ExteriorColor != null)
            {
                //vehicles = context.Vehicles.Where(p => p.ExteriorColor.Contains(search.ExteriorColor));
                vehicles = from v in vehicles
                           where search.ExteriorColor == v.ExteriorColor
                           select v;
            }
            if (search.InteriorColor != null)
            {
                //vehicles = context.Vehicles.Where(p => p.InteriorColor.Contains(search.InteriorColor));
                vehicles = from v in vehicles
                           where search.InteriorColor == v.InteriorColor
                           select v;
            }
            if (search.MinMileage.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.Mileage >= search.MinMileage);
                vehicles = from v in vehicles
                           where v.Mileage >= search.MinMileage
                           select v;
            }
            if (search.MaxMileage.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.Mileage <= search.MaxMileage);
                vehicles = from v in vehicles
                           where v.Mileage <= search.MaxMileage
                           select v;
            }
            if (search.EngineType != null)
            {
                //vehicles = context.Vehicles.Where(p => p.EngineConfiguration == search.EngineType).Select(p => p);
                vehicles = from v in vehicles
                           where search.EngineType == v.EngineConfiguration
                           select v;
            }
            if (search.FuelType != null)
            {
                //vehicles = context.Vehicles.Where(p => p.FuelType == search.FuelType).Select(p => p);
                vehicles = from v in vehicles
                           where search.FuelType == v.FuelType
                           select v;
            }
            if (search.LowPrice.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.Price >= search.LowPrice);
                vehicles = from v in vehicles
                           where v.Price >= search.LowPrice
                           select v;
            }
            if (search.HighPrice.HasValue)
            {
                //vehicles = context.Vehicles.Where(p => p.Price <= search.HighPrice);
                vehicles = from v in vehicles
                           where v.Price <= search.HighPrice
                           select v;
            }

            return vehicles.ToList();
        }
    }
}
