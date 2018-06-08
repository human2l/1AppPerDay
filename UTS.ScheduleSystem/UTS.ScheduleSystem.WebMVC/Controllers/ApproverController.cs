using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class ApproverController : Controller
    {
        private ApproverService approverService = new ApproverService();
        private string currentUser = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Approver
        public ActionResult Index()
        {
            // Read all rule into list
            ViewBag.ConversationalRules = approverService.RequestPendingConversationalRulesList();
            ViewBag.FixedConversationalRules = approverService.RequestPendingFixedConversationalRulesList();
            return CheckCurrentUser();
        }

        // GET: Approver/Approve
        // Approve a fixed rule
        public ActionResult ApproveFixedConversationalRule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            approverService.ApproveFixedConversationalRule(id.ToString());
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
            approverService.ApproveConversationalRule(id.ToString());
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
            approverService.RejectRuleInFixedConversationalRuleList(id.ToString());
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
            approverService.RejectRuleInConversationalRuleList(id.ToString());
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
            return CheckCurrentUser();
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
                approvedCount.Add(approverService.UserRelatedApprovedRulesNum(editor.Email));
                rejectedCount.Add(approverService.UserRelatedRejectedRulesNum(editor.Email));
                pendingCount.Add(approverService.UserRelatedPendingRulesNum(editor.Email));
                successRate.Add(approverService.UserSuccessRate(editor.Email) *100);
            }
            ViewBag.editorList = editors;
            ViewBag.approvedCount = approvedCount;
            ViewBag.rejectedCount = rejectedCount;
            ViewBag.pendingCount = pendingCount;
            ViewBag.successRate = successRate;
            ViewBag.overallSuccessRate = approverService.OverallAveSuccessRate()*100;
            return CheckCurrentUser();
        }

        // Check the role of current user
        public ActionResult CheckCurrentUser()
        {
            if (currentUser != "" && UserHandler.GetCurrentUserRole(currentUser).Contains("A"))
            {
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}