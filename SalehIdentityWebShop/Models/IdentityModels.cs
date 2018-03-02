using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SalehIdentityWebShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /*in IdentityUser we do not have first and last name 
         * 'and address  but we have PhoneNymber then we add just those we do not have it*/

        [Range(minimum: 0, maximum: 150)]
        public int Age { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(length: 40)]
        [MinLength(length: 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(length: 40)]
        [MinLength(length: 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public List<Order> Orders { get; set; }//
        public Cart Cart { get; set; }


        public ApplicationUser(int age, string firstName, string lastName, string address)
        {
            Age = age;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public ApplicationUser()
        {
        }

        // now we have to added to AcountViewModel but we need to make Update-database before because we add new proprties  
        /* then we will add to AcountViewModel register and AcountController register and Views in register and we have 
        * to go back to seed to give new properties same we added here to both new users because those are required */

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //Saleh  For Add user to role we forllow this link //https://www.youtube.com/watch?v=IngL0-alQYk&list=PL-EU0JUF-XD2BpvdS_ognd6PiSoFX4k5_&index=11 
    //*We start from here  to make ApplicationRole here to assign Role to User 
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base() { }
        public ApplicationRole(string roleName) :base (roleName) { } 
    }// Jump to IdentityConfig.cs to last line to add

    /*we need to change the name of this class and the constractor dawon by same name, that will
     * help to connect the database with name of ConnectionString, we have this name */
    //then we use this name of class to declare a db reference to fitch tables in DataBase 
    public class WebShopDbContext : IdentityDbContext<ApplicationUser>
    {
        //we have to change the name of this up class to not be look like name of project 
        public WebShopDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static WebShopDbContext Create()
        {
            return new WebShopDbContext();
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //if we need to connect with user we go upp to 'ApplicationUser'
        //public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}