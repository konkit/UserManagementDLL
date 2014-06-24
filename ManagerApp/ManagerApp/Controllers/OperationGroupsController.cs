using ManagerApp.Models;
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
    public class OperationGroupsController : BaseController
    {
        private GroupManager gm = new GroupManager(new ManagerContext());
        
        // GET: OperationGroups
        public ActionResult Index()
        {
            return View(gm.DisplayGropus());
        }
        
        // GET: OperationGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationGroup operationGroup = gm.FindGroup(id);
            if (operationGroup == null)
            {
                return HttpNotFound();
            }
            return View(operationGroup);
        }
        
        // GET: OperationGroups/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: OperationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] OperationGroup operationGroup)
        {
            if (ModelState.IsValid)
            {
                gm.CreateGroup(operationGroup);
                return RedirectToAction("Index");
            }
        
            return View(operationGroup);
        }
        
        // GET: OperationGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationGroup operationGroup = gm.FindGroup(id);
            if (operationGroup == null)
            {
                return HttpNotFound();
            }
            return View(operationGroup);
        }
        
        // POST: OperationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] OperationGroup operationGroup)
        {
            if (ModelState.IsValid)
            {
                gm.EditGroup(operationGroup);
                return RedirectToAction("Index");
            }
            return View(operationGroup);
        }
        
        // GET: OperationGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationGroup operationGroup = gm.FindGroup(id);
            if (operationGroup == null)
            {
                return HttpNotFound();
            }
            return View(operationGroup);
        }
        
        // POST: OperationGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gm.DeleteGroup(id);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            gm.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
