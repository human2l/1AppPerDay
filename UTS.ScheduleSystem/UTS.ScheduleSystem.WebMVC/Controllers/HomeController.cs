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
                //ViewBag.Answer = "find answer of input: " + models.Question;
                ViewBag.Answer = Conversation(models.Question);
            }
            else
            {
                ViewBag.Answer = "Please type something!";
            }

            return View();

        }

        private string Conversation(string input)
        {
            string output = "";
            input = MainLogic.Utils.RemoveAllMarks(input);
            input = MainLogic.Utils.IgnoreWhiteSpace(input);
            output = input;
            return output;
        }

    }
}