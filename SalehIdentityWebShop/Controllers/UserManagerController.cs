using SalehIdentityWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalehIdentityWebShop.Controllers
{
    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserManager
        public ActionResult Index()
        {
            //to display users table we need to use ApplicationUser class 
            List<ApplicationUser> myUser = new List<ApplicationUser>();

            myUser = db.Users.OrderBy(u=>u.UserName).ToList();
            return View(myUser);//to display table 
        }

        // GET: UserManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManager/Create
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

        // GET: UserManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserManager/Edit/5
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

        // GET: UserManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserManager/Delete/5
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
    }
}
