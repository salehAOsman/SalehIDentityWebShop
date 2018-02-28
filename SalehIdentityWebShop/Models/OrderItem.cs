using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalehIdentityWebShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Price { get; set; }//this price connect with this time and date timesPrice it will copy/paste from product in this date If we change the time we have after a year new price but this itemPrice is history price
        public int Amount { get; set; }

        // public virtual Order Orders { get; set; }            // 

        public virtual Product Products { get; set; }        // reference for people 

    }
}