using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalehIdentityWebShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public virtual Product Products { get; set; }            // reference for people 
        
        //public virtual Cart Cart { get; set; }                 // this is if we need to navigat about it opposite database,  navigation property* ----> 1
    }
}
