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
    }
}