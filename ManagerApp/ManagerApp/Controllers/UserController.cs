﻿using DatabaseContext;
using UserDataLib.Security;
using Newtonsoft.Json;
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
    [CustomAuthorize(Groups = "Administrators,Users,")]
    
    public class UserController : BaseController
    {
        private UserManager um ;

        public UserController()
        {
            um = new UserManager(new DBContext());
        }

        [Authorize]
        [CustomAuthorize(Roles = "DisplayUser")]
        public ActionResult Index()
        {
            
            //UserMenager.cs
            return View(um.DisplayUser());
        }
        [CustomAuthorize(Roles = "ShowDetails")]
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
            //if (user.Id != int.Parse(Session["userId"].ToString()))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            return View(user);
        }
        public ActionResult Activation(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = um.Find(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activation([Bind(Include = "Id,Username,Password,data")] User model)
        {
            
            
                um.ChangeActivation(model);
                return RedirectToAction("Details", new { Id = model.Id });
            
        }

        public ActionResult AddOperation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operations operations = new Operations { Id = (int)id };
            return View(operations);
        }


        [HttpPost]
        public ActionResult AddOperation(Operations model)
        {
            if (ModelState.IsValid)
            {
                um.AddOperation(model.Id, model.OperationId);

                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);
        }

        public ActionResult DeleteOperation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operations operations = new Operations { Id = (int)id };
            return View(operations);
        }


        [HttpPost]
        public ActionResult DeleteOperation(Operations model)
        {
            if (ModelState.IsValid)
            {
                um.DeleteOperation(model.Id, model.OperationId);

                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);
        }

        public ActionResult AddGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups Groups = new Groups { Id = (int)id };
            return View(Groups);
        }


        [HttpPost]
        public ActionResult AddGroup(Groups model)
        {
            if (ModelState.IsValid)
            {
                um.AddGroup(model.Id, model.GroupId);

                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);
        }

        public ActionResult DeleteGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups Groups = new Groups { Id = (int)id };
            return View(Groups);
        }


        [HttpPost]
        public ActionResult DeleteGroup(Groups model)
        {
            if (ModelState.IsValid)
            {
                um.DeleteGroup(model.Id, model.GroupId);

                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(LoginViewModel user,  string returnUrl = "")
        {      
            
            if(ModelState.IsValid)  
            {
                bool isValid = um.LoginUserIsValid(user);
                if(isValid)     
                {
                    if(um.ChangeActiveAccount(user))
                    {
                        var modelUser = um.getUser(user.Username, user.Password);

                        var operations = modelUser.Operations.Select(m => m.Name).ToArray();
                        var groups = modelUser.OperationGroups.Select(g => g.Name).ToArray();

                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                        serializeModel.UserId = modelUser.Id;
                        serializeModel.Username = modelUser.Username;
                        serializeModel.operations = operations;
                        serializeModel.groups = groups;

                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                 1,
                                 modelUser.Username,
                                 DateTime.Now,
                                 DateTime.Now.AddMinutes(15),
                                 false,
                                 userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        if (operations.Contains("admin"))
                        {
                            return RedirectToAction("Index", "User");
                        }
                        //FormsAuthentication.SetAuthCookie(user.Username, true);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account is not active! ");
                    }
                    
                }   
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect! ");
                }
                
            }
            else
                ModelState.AddModelError("", "Error");
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [CustomAuthorize(Roles = "CreateUser")]
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

        [CustomAuthorize(Roles = "Edit")]
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
        public ActionResult Edit([Bind(Include="Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                //UserMenager.cs
                um.EditUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [CustomAuthorize(Roles = "DeleteObject")]
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
