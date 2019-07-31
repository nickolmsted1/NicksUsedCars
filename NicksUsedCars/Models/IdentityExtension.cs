using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public static class IdentityExtension
    {
        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");
            var claim2 = ((ClaimsIdentity)identity).FindFirst("LastName");

            return (claim != null && claim2 != null) ? claim.Value + " " + claim2.Value : "Valued User";
        }
    }
}
