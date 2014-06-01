using ManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserDataLib.Models;
using UserDataLib.Services;

namespace ManagerApp.Controllers
{
    
    public class UserController : Controller
    {
        private UserManager um = new UserManager(new ManagerContext());
       
     
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
            if (user.Id != int.Parse(Session["userId"].ToString()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel user)
        {      
            
            if(ModelState.IsValid)
            {
                bool isValid = um.LoginUserIsValid(user);
                if(isValid)
                {
                    
                    
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                   
                    return RedirectToAction("Index", "Home");
                }   
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
                
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,ConfirmPassword")] RegisterViewModel user)
        {            
            if (ModelState.IsValid)
            {   
                if(!(um.UserNameIsNoExists(user)))
                {
                    ModelState.AddModelError("", "The data is incorrect or the user with the specified name already exists in the database!");
                }
                else
                {
                    
                    
                    //UserMenager.cs
                    um.CreateUser(user);
                    return RedirectToAction("Index", "Home");
                }
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
