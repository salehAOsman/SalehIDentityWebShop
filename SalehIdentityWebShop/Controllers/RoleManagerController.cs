using Microsoft.AspNet.Identity.EntityFramework;
using SalehIdentityWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalehIdentityWebShop.Controllers
{
    public class RoleManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: RoleManager
        public ActionResult Index()
        {
            //to display role table we need to use IdentityRole class 
            List<IdentityRole> myRole = new List<IdentityRole>();
            myRole = db.Roles.OrderBy(r=>r.Name).ToList();

            return View(myRole);//to display table
        }

        // GET: RoleManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleManager/Create
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

        // GET: RoleManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleManager/Edit/5
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

        // GET: RoleManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleManager/Delete/5
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
