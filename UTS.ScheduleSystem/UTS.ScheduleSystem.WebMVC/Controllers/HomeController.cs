using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.WebMVC.Models;


namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private HomeControllerViewModels _model;
        public HomeController()
        {
            _model = new HomeControllerViewModels { Question = "question" };
        }
        public ActionResult Index()
        {
            ViewBag.Answer = "I'm waiting...";
            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeControllerViewModels models)
        {
            if (models.Question != null)
            {
                ViewBag.Answer = "find answer of input: " + models.Question;
            }
            else
            {
                ViewBag.Answer = "Please type something!";
            }

            return View();

        }

    }
}