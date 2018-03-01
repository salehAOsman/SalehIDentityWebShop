using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// we come from Start.Auth.cs 
namespace SalehIdentityWebShop.Models
{
    public class RoleViewModel
    {
        //we need this viewModel to display role of user in view 
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role) //constractor to give not empty role by overload role object
        {
            Id = role.Id;
            Name = role.Name;
        }

    }
}
//we jump to controller and create new controller select 'Empty one' or crud, the name is Role 
