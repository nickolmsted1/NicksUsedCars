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

        public static List<ApplicationUser> SearchUsers(NicksUsedCarsContext context, UserSearch searchResults)
        {
            if (!string.IsNullOrWhiteSpace(searchResults.FirstName) && !string.IsNullOrWhiteSpace(searchResults.LastName))
            {
                IQueryable<ApplicationUser> users = context.Users.Select(user => user);

                users = from user in users
                        where user.FirstName == searchResults.FirstName
                            && user.LastName == searchResults.LastName
                        select user;
                if (!string.IsNullOrWhiteSpace(searchResults.Email))
                {
                    users = from user in users
                            where user.Email == searchResults.Email
                            select user;
                }

                return users.ToList();
            }
            return null;
        }
    }
}
