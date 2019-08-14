using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public class UserSearch
    {
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Please enter only letters into search")]
        [Required(ErrorMessage = "Please enter a value")]
        public string FirstName { get; set; }

        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Please enter only letters into search")]
        [Required(ErrorMessage = "Please enter a value")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public List<ApplicationUser> UserSearchResults { get; set; }
    }
}
