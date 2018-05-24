using System;
using System.Collections.Generic;
using System.Linq;
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
            editorService.AddNewFCRule(rule.Input, rule.Output, "bd6ba246-7bfb-4ebb-a2f2-0c6714be88bb");
            return RedirectToAction("Index");
        }

        //public ActionResult Edit(int? id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Edit(PersonalContact contact)
        //{
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Delete(int? id)
        //{
        //    return View(contact);
        //}

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    return RedirectToAction("Index");
        //}
    }
}