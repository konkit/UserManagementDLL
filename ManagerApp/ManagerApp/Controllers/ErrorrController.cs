using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerApp.Controllers
{
    public class ErrorrController : Controller
    {
        //
        // GET: /Error/
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}