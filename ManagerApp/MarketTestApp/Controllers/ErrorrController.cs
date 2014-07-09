using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketTestApp.Controllers
{
    public class ErrorrController : Controller
    {
        //
        // GET: /Errors/
        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult LoggedOut()
        {
            return RedirectToAction("login", "User");
        }

        public ActionResult NullPoint()
        {
            return RedirectToAction("Index", "Home");
        }
	}
}