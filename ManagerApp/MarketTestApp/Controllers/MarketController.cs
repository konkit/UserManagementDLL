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
using MarketTestApp.Security;
using DatabaseContext;

namespace MarketTestApp.Controllers
{
    public class MarketController : Controller
    {
        private DBContext db;
        private UserManager um;

        public MarketController()
        {
             db = new DBContext();
             um = new UserManager(db);
        }

        // GET: /Market/
        public ActionResult Index()
        {
            return RedirectToAction("Buy");
        }

        [CustomAuthorize(Roles = "CreateItem")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Market/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Item.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }
        [CustomAuthorize(Roles = "DoBuy")]
        public ActionResult Buy()
        {
            return View(db.Item.ToList());
        }

        
        public ActionResult DoBuy(int? id)
        {
            User currentUser = um.GetUser();

            if (currentUser == null)
            {
                return RedirectToAction("LoggedOut", "Errors");
            }

            ItemPossession poss = new ItemPossession();
                poss.Item = db.Item.Find(id);
                poss.UserId = currentUser.Id;
                poss.Quantity = 1;
            db.ItemPossession.Add(poss);
            db.SaveChanges();

            return RedirectToAction("Buy");
        }

        [CustomAuthorize(Roles = "DoSell")]
        public ActionResult Sell()
        {
            User currentUser = um.GetUser();

            if (currentUser == null)
            {
                return RedirectToAction("LoggedOut", "Errors");
            }

            List<Item> outputItems = db.ItemPossession.Where(x => x.User.Id == currentUser.Id).Select(x => x.Item).ToList();

            return View(outputItems);
        }

        
        public ActionResult DoSell(int id)
        {
            User currentUser = um.GetUser();

            if (currentUser == null)
            {
                return RedirectToAction("LoggedOut", "Errors");
            }

            var poss = db.ItemPossession.First(x => x.User.Id == currentUser.Id && x.Item.Id == id);
            db.ItemPossession.Remove(poss);
            db.SaveChanges();

            return RedirectToAction("Sell");
        }
    }
}
