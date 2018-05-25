using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.WebMVC.Models;

namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class EditorController : Controller
    {
        MainLogic.EditorService editorService = new MainLogic.EditorService();
        
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
                editorService.AddNewFCRule(rule.Input.ToLower(), rule.Output.ToLower(), "bd6ba246-7bfb-4ebb-a2f2-0c6714be88bb");
            }
            else if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output))
            {
                editorService.AddNewFCRule(rule.Input.ToLower(), rule.Output.ToLower(), "bd6ba246-7bfb-4ebb-a2f2-0c6714be88bb");
            }
            else
            {
                // Show error message
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = editorService.FindRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.OnEditingRule = rule;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Rule rule)
        {
            if (editorService.IsFixedRuleValid(rule.Input, rule.Output))
            {
                editorService.EditPendingRule("123", rule.Id + "", rule.Input, rule.Output);
            }
            else if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output))
            {
                editorService.EditPendingRule("123", rule.Id + "", rule.Input, rule.Output);
            }
            else
            {
                // Show error message
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rule = editorService.FindRuleById(id + "");
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
            var rule = editorService.FindRuleById(id + "");
            if (rule == null)
            {
                return HttpNotFound();
            }
            editorService.DeletePendingRule(id + "");
            return RedirectToAction("Index");
        }
    }
}