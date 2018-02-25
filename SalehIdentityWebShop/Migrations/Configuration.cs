namespace SalehIdentityWebShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SalehIdentityWebShop.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalehIdentityWebShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        //here we change 'contect' as default  to 'db' in seed method  like this 
        //protected override void Seed(SalehIdentityWebShop.Models.ApplicationDbContext context)
        protected override void Seed(SalehIdentityWebShop.Models.ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //Saleh *2 first step for full project Identity 

            ApplicationUser myAdmin;//we need object refer to users class
            ApplicationUser myFoo;
            //I need to create new roleStore  to create new role manager
            var roleStore = new RoleStore<IdentityRole>(db);
            //I need to create new roleManager  to create new userManager
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // //now we will create a roles not users 
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (roleManager.FindByName("Guru") == null)//check if it is first time then create new user
            {
                roleManager.Create(new IdentityRole("Guru"));// my role for adminstration
            }
            if (roleManager.FindByName("Common") == null)//check if it is first time then create new user
            {
                roleManager.Create(new IdentityRole("Common"));// my role for a normal User
            }

            //now we will create a users 

            if (userManager.FindByName("Admin") == null)
            {
                //we need new object from ApplicationUser to creat new user 
                myAdmin = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "admin@admin.se",
                    //Age = 99,
                    //FirstName = "Admin",
                    //LastName = "Administration",
                    //Adress = "Admi-role 1"
                };
                userManager.Create(myAdmin, "!23Qwe");      //we add now to Database
            }
            //another user
            if (userManager.FindByName("Foo") == null)
            {
                //we need new object from ApplicationUser to creat new user but we will change the 
                //ApplicationUser myFoo = new ApplicationUser();
                myFoo = new ApplicationUser()
                {
                    UserName = "Foo",
                    Email = "foo@foo.se",
                    //Age = 0,
                    //FirstName = "Foo",
                    //LastName = "Foo",
                    //Adress = "Foo-road 1"
                };
                userManager.Create(myFoo, "!23Qwe");      //we add now to Database
            }

            //here save both to database 
            db.SaveChanges();
            //we need to assign user to object
            myAdmin = userManager.FindByName("Admin");
            myFoo = userManager.FindByName("Foo");
            //we will assign role as "Guru" to "myadmin" user 
            userManager.AddToRole(myAdmin.Id, "Guru");
            userManager.AddToRole(myFoo.Id, "Common");
            //to see data base go PM>Update-Database
        }
    }
}
