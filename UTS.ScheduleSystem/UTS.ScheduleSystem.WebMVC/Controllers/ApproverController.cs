using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class ApproverController : Controller
    {
        // GET: Approver
        public ActionResult Index()
        {
            // Read all rule into list
            ViewBag.ConversationalRules = ConversationalRuleHandler.FindAllConversationalRules();
            ViewBag.FixedConversationalRules = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            return View();
        }
    }
}