using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? input)
        {
            if (input != null)
            {
                ViewBag.Answer = "find answer of input: " + input;
            }

            return View();
        }

    }
}