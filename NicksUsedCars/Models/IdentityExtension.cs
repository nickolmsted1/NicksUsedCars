using Microsoft.AspNetCore.Identity;
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
        
        public static ApplicationUser GetUser(NicksUsedCarsContext context, string id)
        {
            ApplicationUser user = context.Users.Where(u => u.Id == id).Single();
            return user;
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
                //users = users.ToList();
                //for (int i = 0; i < users.Count(); i++)
                //{
                //    ApplicationUser name;
                //}
                return users.ToList();
            }
            return null;
        }

        public static async Task<List<string>> GetRoles(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            List<string> roles = (List<string>) await userManager.GetRolesAsync(user);
            return roles;
        }

        public static async Task<bool> IsAdmin(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            return isAdmin;
        }

        public static async Task<bool> IsManagerOrAbove(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var isManager = await userManager.IsInRoleAsync(user, "Manager");
            return isManager || await IsAdmin(userManager, user);
        }

        public static async Task<bool> IsEmployeeOrAbove(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var isEmployee = await userManager.IsInRoleAsync(user, "Employee");
            return isEmployee || await IsManagerOrAbove(userManager, user);
        }

        public static async Task<bool> IsCustomer(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var isCustomer = await userManager.IsInRoleAsync(user, "Customer");
            return isCustomer;
        }
    }
}
