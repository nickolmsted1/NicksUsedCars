using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string RoleName { get; set; }

        public string FullName { get; set; }

        public List<string> OldRoles { get; set; }
    }
}
