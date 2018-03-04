using System;
using SalehIdentityWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SalehIdentityWebShop.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        //this name of lcass that include the line and name of connectionString in IdentityModels.cs
        private WebShopDbContext db = new WebShopDbContext();//get Users, Roles

        // GET: UserManager
        public ActionResult Index(string orderBy)
        {
            List<ApplicationUser> myUser = new List<ApplicationUser>();
            if (string.IsNullOrEmpty(orderBy))
            {
                ViewBag.OrderNameBy = "NameA";
                myUser = db.Users.OrderBy(u => u.UserName).ToList();
            }
            else
            {
                switch (orderBy)
                {
                    case "NameA":// order up
                        myUser = db.Users.OrderBy(u => u.UserName).ToList();
                        ViewBag.OrderNameBy = "NameD";
                        break;
                    case "NameD"://order dawon 
                        myUser = db.Users.OrderByDescending(u => u.UserName).ToList();
                        ViewBag.OrderNameBy = "NameA";
                        break;

                    default:

                        break;
                }
            }
            return View(myUser);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userStore = new UserStore<ApplicationUser>(db);                 //what does it means ???
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(id);


            //ApplicationUser user=db.Users.Include("Roles").SingleOrDefault(s => s.Id == id);

            if (user==null)
            {
                return HttpNotFound();
            }
            IList<string> usersRoles = userManager.GetRoles(id);

            UserViewModel VM_User = new UserViewModel(user, usersRoles);

            return View(VM_User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRoleFromUser(string name,string uId)
        {

            if (name == null || uId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleViewModel myViewModel = new RoleViewModel();

            

            return View();
        }

    }
}
