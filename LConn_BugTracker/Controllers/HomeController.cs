using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LConn_BugTracker.Models;

namespace LConn_BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }




    }
}