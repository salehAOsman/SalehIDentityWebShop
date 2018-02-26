using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// we come from Start.Auth.cs 
namespace SalehIdentityWebShop.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
         
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}//we jump to controller and create new controller select 'Empty one' or crud, the name is Role 
