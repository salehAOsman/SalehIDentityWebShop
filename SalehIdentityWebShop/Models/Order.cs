using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SalehIdentityWebShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // We use the "ApplicationUser" instead of "User" class Because it has default name as Identity
        // public virtual ApplicationUser UserApp { get; set; }     //Forgen key "UserApp" if we need to now how is the user for this Order       //reference to people

        public List<OrderItem> OrderItems { get; set; }          //      add columns from Order as list for mor details
        
    }
}
