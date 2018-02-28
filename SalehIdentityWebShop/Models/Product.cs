using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalehIdentityWebShop.Models
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter the description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the quantity that Available in the storage ")] //Amount
        public int AvailableStorage { get; set; }

        public string Image { get; set; }
    }
}