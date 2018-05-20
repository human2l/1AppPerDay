using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.MainLogic;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class ApproverController : Controller
    {
        private ApproverService approverService = new ApproverService();

        // GET: Approver
        public ActionResult Index()
        {
            // Read all rule into list
            ViewBag.ConversationalRules = approverService.RequestPendingConversationalRulesList();
            ViewBag.FixedConversationalRules = approverService.RequestPendingFixedConversationalRulesList();
            return View();
        }

        // GET: Approver/Approve
        // Approve a rule
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.ApproveRule(id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Approver/Reject
        // Reject a rule
        public ActionResult Reject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.RejectRule(id.ToString());
            return RedirectToAction("Index");
        }
    }
}