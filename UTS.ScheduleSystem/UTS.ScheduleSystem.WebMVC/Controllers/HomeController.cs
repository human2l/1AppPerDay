using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.WebMVC.Models;
using System.Web.Security;


namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Answer = Membership.GetUser().ToString();
            if(System.Web.HttpContext.Current != null)
            ViewBag.Answer = "Hello, " + System.Web.HttpContext.Current.User.Identity.Name;
            //ViewBag.Answer = "I'm waiting...";
            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeControllerViewModels models)
        {
            if (models.Question != null)
            {
                //ViewBag.Answer = "find answer of input: " + models.Question;
                ViewBag.Answer = HandleConversation(models.Question);
            }
            else
            {
                ViewBag.Answer = "Please type something!";
            }

            return View();

        }

        // Handle main conversation function
        private string HandleConversation(string input)
        {
            input = DomainLogic.Utils.RemoveAllMarks(input);
            input = DomainLogic.Utils.IgnoreSpace(input);

            DomainLogic.ConversationService conversationService = new DomainLogic.ConversationService();
            string output = conversationService.Conversation(input);
            return output;
        }

    }
}