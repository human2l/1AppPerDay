using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
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
        // Approve a fixed rule
        public ActionResult ApproveFixedConversationalRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.ApproveConversationalRule(id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Approver/Approve
        // Approve a regular rule
        public ActionResult ApproveConversationalRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.ApproveFixedConversationalRule(id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Approver/Reject
        // Reject a fixed rule
        public ActionResult RejectFixedConversationalRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.RejectRuleInConversationalRuleList(id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Approver/Reject
        // Reject a regular rule
        public ActionResult RejectConversationalRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.RejectRuleInFixedConversationalRuleList(id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Approver/ApproverReport
        // Present a report to demonstrate approver data
        public ActionResult ApproverReport()
        {
            ViewBag.ApprovedConversationalRules = approverService.RequestApprovedConversationalRulesList();
            ViewBag.ApprovedFixedConversationalRules = approverService.RequestApprovedFixedConversationalRulesList();
            ViewBag.approvedRulesCount = approverService.ApprovedRulesNum();
            ViewBag.rejectedRulesCount = approverService.RejectedRulesNum();
            ViewBag.successRate = approverService.SuccessRate();
            return View();
        }

        // GET: Approver/EditorReport
        // Present a report to demonstrate editor data
        public ActionResult EditorReport()
        {
            List<AspNetUser> editors = approverService.RequestEditorList();
            List<int> approvedCount = new List<int>();
            List<int> rejectedCount = new List<int>();
            List<int> pendingCount = new List<int>();
            List<double> successRate = new List<double>();
            foreach (var editor in editors)
            {
                approvedCount.Add(approverService.UserRelatedApprovedRulesNum(editor.Id));
                rejectedCount.Add(approverService.UserRelatedRejectedRulesNum(editor.Id));
                pendingCount.Add(approverService.UserRelatedPendingRulesNum(editor.Id));
                successRate.Add(approverService.UserSuccessRate(editor.Id)*100);
            }
            ViewBag.editorList = editors;
            ViewBag.approvedCount = approvedCount;
            ViewBag.rejectedCount = rejectedCount;
            ViewBag.pendingCount = pendingCount;
            ViewBag.successRate = successRate;
            ViewBag.overallSuccessRate = approverService.OverallAveSuccessRate()*100;
            return View();
        }
    }
}