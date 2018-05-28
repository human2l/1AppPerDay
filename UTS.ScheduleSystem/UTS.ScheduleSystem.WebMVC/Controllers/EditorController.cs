using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.WebMVC.Models;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;


namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class EditorController : Controller
    {
        private MainLogic.EditorService editorService = new MainLogic.EditorService();
        private string currentUser = System.Web.HttpContext.Current.User.Identity.Name;

        // GET: Editor
        public ActionResult Index()
        {
            List<FixedConversationalRule> fixedConversationalRules = editorService.ShowAllFixedConversationalRuleRules();
            List<ConversationalRule> conversationalRules = editorService.ShowAllConversationalRuleRules();
            //FixedConversationalRule rule = new FixedConversationalRule();
            //rule.Id = 1;
            //rule.Input = "Hello";
            //rule.Output = "World";
            //rule.RelatedUsersId = "u001";
            //rule.Status = "Pending";
            //rules.Add(rule);
            ViewBag.FixedConversationalRule = fixedConversationalRules;
            ViewBag.ConversationalRule = conversationalRules;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Input, Output")] Rule rule)
        {
            rule.Status = "Pending";
            if (editorService.IsFixedRuleValid(rule.Input, rule.Output))
            {
                editorService.AddNewFCRule(rule.Input.ToLower(), rule.Output.ToLower(), currentUser);
            }
            else if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output))
            {
                editorService.AddNewCRule(rule.Input.ToLower(), rule.Output.ToLower(), currentUser);
            }
            else
            {
                // Show error message
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = ConversationalRuleHandler.FindConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnEditingRule = rule;
            return View();
        }

        public ActionResult EditFixed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnEditingRule = rule;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ConversationalRule rule)
        {
            rule.Status = "Pending";
            rule.RelatedUsersId = ConversationalRuleHandler.FindConversationalRuleById(rule.Id + "").RelatedUsersId + " " + currentUser;
            if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output))
            {
                ConversationalRuleHandler.UpdateAConversationalRule(rule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        public ActionResult EditFixed(FixedConversationalRule rule)
        {
            rule.Status = "Pending";
            rule.RelatedUsersId = FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id + "").RelatedUsersId + " " + currentUser;
            if (editorService.IsFixedRuleValid(rule.Input, rule.Output))
            {
                FixedConversationalRuleHandler.UpdateAFixedConversationalRule(rule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = ConversationalRuleHandler.FindConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnDeletingRule = rule;
            return View();
        }

        public ActionResult DeleteFixed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnDeletingRule = rule;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var rule = ConversationalRuleHandler.FindConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ConversationalRuleHandler.RemoveConversationalRule(id + "");
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteFixed")]
        public ActionResult DeleteFixedConfirmed(int id)
        {
            var rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            FixedConversationalRuleHandler.RemoveFixedConversationalRule(id + "");
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            List<Rule> currentUserApprovedRules = editorService.ShowCurrentUserApprovedRules(currentUser);
            ViewBag.CurrentUserApprovedRules = currentUserApprovedRules;
            ViewBag.CurrentUserApprovedRulesCount = editorService.ShowCurrentUserApprovedRulesCount(currentUser);
            ViewBag.CurrentUserRejectedRulesCount = editorService.ShowCurrentUserRejectedRulesCount(currentUser);
            ViewBag.SuccessRate = editorService.ShowCurrentUserSuccessRate(currentUser);
            return View();
        }
    }
}