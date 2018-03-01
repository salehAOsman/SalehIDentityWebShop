using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalehIdentityWebShop.Models;
using Microsoft.AspNet.Identity;

namespace SalehIdentityWebShop.Controllers
{
    public class CartsController : Controller
    {
        private WebShopDbContext db = new WebShopDbContext();

        // GET: Carts
        //public ActionResult Index()
        //{
        //    return View(db.Carts.ToList());
        //}

        // GET: Carts/Details/5
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Include("Cart").Include("Cart.CartItems").Include("Cart.CartItems.Products").SingleOrDefault(u => u.Id == userId);

            if (user.Cart == null)
            {
                return HttpNotFound();
            }
            return View(user.Cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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
        
        //2*
        [HttpGet]
        public ActionResult ProductToCart(int? pId,string op)       // input variable from view           Add Product To Cart
        {
            var userId= User.Identity.GetUserId();                                  //we have id of user by this code 
            var user = db.Users.Include("Cart").Include("Cart.CartItems").SingleOrDefault(u=>u.Id == userId);// we have now user Object that has the product that he selected

            if (user.Cart == null)
            {
                user.Cart = new Cart();
            }

            bool notFound = true;                                                    //we will check if he has already product inside the cart or not

            foreach (var item in user.Cart.CartItems)                                //we will check in cart by loop 
            {
                if (item.Products.Id== pId)                                                   //product id
                {
                    if (op == "minus")
                    {
                        item.Amount--;
                        if (item.Amount<1)
                        {
                            //user.Cart.CartItems.Remove(item); //history it does not delete data from data base it is like history 
                            db.CartItems.Remove(item);
                        }
                    }
                    else
                    {
                        item.Amount++;                                                  // if it found then add amount 1 more
                    }
                    notFound = false;                                               // then give false to variable to not go inside next block in cart
                    break;                                                          // do not look for other products because we focus about this product
                }
            }
            if (notFound && op != "minus") 
            {
                Product product = db.Products.SingleOrDefault(a => a.Id == pId);      // we found product then we will fitch id 
                CartItem newItem = new CartItem();                                    // new cartItem
                newItem.Amount = 1;                                                   //first pice of product
                newItem.Products = product;                                           //this product we will assign it to this object newItem from ItemCart
                user.Cart.CartItems.Add(newItem);                                     // we add this object as product to cart  
            }
            db.SaveChanges();
            //I try to fix when call it from product side then it has to get back to Product Index view 
            if (op=="bToP")
            {
                return RedirectToAction("Index","Products");
            }
             return RedirectToAction("Details", new { id = user.Cart.Id });
        }
    }
}
