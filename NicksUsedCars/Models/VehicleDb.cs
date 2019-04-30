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
    }
}
