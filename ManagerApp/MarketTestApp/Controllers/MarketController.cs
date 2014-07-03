using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketTestApp.Models;
using UserDataLib.Models;
using UserDataLib.Services;
using MarketTestApp.Security;

namespace MarketTestApp.Controllers
{
    public class MarketController : Controller
    {
        private MarketAppContext db;
        private UserManager um;

        public MarketController()
        {
             db = new MarketAppContext();
             um = new UserManager(db);
        }

        // GET: /Market/
        public ActionResult Index()
        {
            return RedirectToAction("Buy");
        }

        // GET: /Market/Create
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

        public ActionResult Buy()
        {
            return View(db.Item.ToList());
        }

        [CustomAuthorize(Roles = "DoBuy")]
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

        [CustomAuthorize(Roles = "DoSell")]
        public ActionResult DoSell(int id)
        {
            User currentUser = um.GetUser();

            if (currentUser == null)
            {
                return RedirectToAction("LoggedOut", "Errors");
            }

            Item item = db.Item.First(x => x.Id == id);

            ItemPossession poss = db.ItemPossession.First(x => x.User == currentUser && x.Item == item);
            db.ItemPossession.Remove(poss);
            db.SaveChanges();

            return RedirectToAction("Buy");
        }
    }
}
