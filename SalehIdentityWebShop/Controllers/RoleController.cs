using Microsoft.AspNet.Identity.Owin;

using SalehIdentityWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SalehIdentityWebShop.Controllers
{
    public class RoleController : Controller
    {
        //after we create new controller here jump to AcountController to copy/paste few methods : and pasted here  
        // GET: Role
        //After paste here we change to be like this 
        //we change word 'user' to 'role' and 'acount' to 'role' in every places   
        private ApplicationRoleManager _roleManager;
        
        public RoleController()
        {

        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        //Change SignIn to Role
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();// to fix error here we add using up for asp.net
            }
            private set
            {
                _roleManager = value;
            }
        }
        /*Jump Tools to install EntityFrameWork then  to PM>add-migration role and PM>Update-DataBase
         * then we we will build this  Index method down*/
        
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();       //we add using statment 
            foreach (var role in RoleManager.Roles)
            list.Add(new RoleViewModel(role));
            return View(list);
        }

        // Change this to create
        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        //we use here async
        // POST: Role/Create
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
           
                return RedirectToAction("Index");
        }

        //continue to build this
        
        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            return View(new RoleViewModel(role));
        }

        // POST: Role/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);

            return RedirectToAction("Index");
        }

        //continue to build this
        // GET: Role/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> RoleNameDetails(string name)
        {
            var role = await RoleManager.FindByNameAsync(name);

            return View("Details",new RoleViewModel(role));
        }
        //continue to build this
        // GET: Role/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            return View(new RoleViewModel(role));
        }

        // POST: Role/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
  /*then rebuild project and create view for each method up for index as list and select RoleViewModel 
 * as class and same think for each one but deferent select after this created for views then then we
 * will gor to Create View and hide id in Create View Role then run then we  start to follow next link 
 * https://www.youtube.com/watch?v=K3hmX6vXeCk&list=PL-EU0JUF-XD2BpvdS_ognd6PiSoFX4k5_&index=12 to
 * add user and assing role to users we jump to  AcountController.cs  line 20*/