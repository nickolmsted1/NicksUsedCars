using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class VehicleWaitList
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
