using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserDataLib.Models;
using UserDataLib.Services;

namespace ManagerApp.Controllers
{
    public class UserController : Controller
    {
        private UserManager um = new UserManager();
       
     
        public ActionResult Index()
        {
            //UserMenager.cs
            return View(um.DisplayUser());
        }
      
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UserMenager.cs
            User user = um.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Username,Password,Salt")] User user)
        {            
            if (ModelState.IsValid)
            {
                //UserMenager.cs
                um.CreateUser(user);                
                return RedirectToAction("Index");
            }
            return View(user);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UserMenager.cs
            User user = um.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
      
        .
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Username,Password,Salt")] User user)
        {
            if (ModelState.IsValid)
            {
                //UserMenager.cs
                um.EditUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
      
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UserMenager.cs
            User user = um.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
      
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //UserMenager.cs
            um.DeleteUser(id);
            return RedirectToAction("Index");
        }
      
        protected override void Dispose(bool disposing)
        {
            //UserMenager.cs
            um.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
