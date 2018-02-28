using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalehIdentityWebShop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem> CartItems { get; set; } // 1 ----> *  we need list of CartItem to show every item inside Cart

        public Cart() // to avoid null CartItem after run project 
        {
            CartItems = new List<CartItem>();
        }

        //public List<Product> Products { get; set; }
        //[ForeignKey("UserApp")]
        //public string UserAppId { get; set; }//we will fitch id of user here 

        // public virtual ApplicationUser UserApp { get; set; }//1 --->1 with Users

        //[ForeignKey("Product")]
        //public int Product_Id { get; set; }// we will fitch id of product here 




    }
}