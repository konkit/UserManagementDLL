using ManagerApp.Models;
using ManagerApp.Security;
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
    
    public class OperationController : Controller
    {
        private OperationManager om = new OperationManager(new ManagerContext());
        [CustomAuthorize(Roles="DisplayOperations")]
        // GET: /Operation/
        public ActionResult Index()
        {
            return View(om.DisplayOperation());
        }
        [CustomAuthorize(Roles = "ShowDetails")]
        // GET: /Operation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = om.FindOperation(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }


        [CustomAuthorize(Roles = "CreateOperation")]
        // GET: /Operation/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // POST: /Operation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOperation([Bind(Include="Id,Name")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                om.CreateOperation(operation);
                return RedirectToAction("Index");
            }
       
            return View(operation);
        }
       
        // GET: /Operation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = om.FindOperation(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }
       
        // POST: /Operation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include="Id,Name")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                om.EditOperation(operation);
                return RedirectToAction("Index");
            }
            return View(operation);
        }
       
       // // GET: /Operation/Delete/5
         public ActionResult Delete(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Operation operation = om.FindOperation(id);
             if (operation == null)
             {
                 return HttpNotFound();
             }
             return View(operation);
         }
       
        // POST: /Operation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DeleteObject")]
        public ActionResult DeleteConfirmed(int id)
        {
            om.DeleteOperation(id);
            return RedirectToAction("Index");
        }
       
        protected override void Dispose(bool disposing)
        {
           om.Dispose(disposing);
           base.Dispose(disposing);
        }
    }
}
