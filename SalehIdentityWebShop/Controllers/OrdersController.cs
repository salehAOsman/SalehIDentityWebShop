using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalehIdentityWebShop.Models;
using Microsoft.AspNet.Identity;//when we have INumurable type then we use UsingSpace from Identity it will give os the same functionality

namespace SalehIdentityWebShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private WebShopDbContext db = new WebShopDbContext();

        // GET: Orders
        
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }//we need to fitch information for orderItem but we need at include products inside it
            Order order = db.Orders.Include("OrderItems").Include("OrderItems.Products").SingleOrDefault(oI=>oI.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //public ActionResult PlaceOderItems(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderItem orderItem = db.OrderItems.Find(id);
        //    if (orderItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (orderItem.Amount < 1)
        //    {
        //        return RedirectToAction("Details", "Orders");
        //    }
        //    // orderItem need loop
        //    foreach (var item in OrderItem)
        //    {
        //        OrderItem orderItem = new OrderItem();//assign DateTime to orderItem table in db  
        //        orderItem.Amount = item.Amount;
        //        orderItem.Price = item.Products.Price;
        //        orderItem.Products = item.Products;//we assign all properties from this product object
        //        order.OrderItems.Add(orderItem); //we assign every sub object 'orderItem' to main object 'order' added every order item to dabaBase  OrderItems table 
        //    }
            
        //    return View();
        //}
        // GET: Orders/Create
        public ActionResult PlaceOrder()
        {
            //we need new object to use it to disply just user Cart, but not all users from dbase 
            var userId = User.Identity.GetUserId();                                                                  // we have id of user by this code to assign his cart to display in recipt not for all users  
            var user = db.Users.Include("Cart").Include("Cart.CartItems").Include("Orders").Include("Orders.OrderItems").SingleOrDefault(u => u.Id == userId); // by this way we fitch the database table
            ViewBag.userName = "FirstName: " + user.FirstName + ",LastName : " + user.LastName;
            if (user.Cart.CartItems.Count < 1)
            {
                return RedirectToAction("Details","Carts");
            }
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            foreach (var item in user.Cart.CartItems)
            {
                OrderItem orderItem = new OrderItem();//assign DateTime to orderItem table in db  
                orderItem.Amount = item.Amount;
                orderItem.Price = item.Products.Price;  
                orderItem.Products = item.Products;//we assign all properties from this product object
                order.OrderItems.Add(orderItem); //added every order item to dabaBase  OrderItems table 
            }
            user.Orders.Add(order); //we add order list to 'user' object
            user.Cart.CartItems.Clear();//reset CartItem list

            db.SaveChanges();
            
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,OrderDate")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}
        // GET: Orders/Edit/5
    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
