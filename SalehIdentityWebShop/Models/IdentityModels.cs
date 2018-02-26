﻿using System.ComponentModel.DataAnnotations;
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

        public string Adress { get; set; }

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
        public ApplicationRole(string roleName) :base (roleName){ } 
    }// Jump to IdentityConfig.cs to last line to add

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.RoleViewModel> RoleViewModels { get; set; }

        //public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.ApplicationUser> ApplicationUsers { get; set; }
        //project created by self this DbSet then we have here tow reference to db one already from identity  and another from Wizard when we create new view for usermanger   
        // public System.Data.Entity.DbSet<SalehIdentityWebShop.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}