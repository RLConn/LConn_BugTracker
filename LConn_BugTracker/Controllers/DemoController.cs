using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace LConn_BugTracker.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult StartDemoPage()
        {
            return View();
        }

        

    }

}