using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalehIdentityWebShop.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; } // 1 ---> * with Roles to display list of role in view user but we need Role viewModel 

        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser user, IList<string> UserRolesNames)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;

            Roles = UserRolesNames.ToList();
        }

    }
}