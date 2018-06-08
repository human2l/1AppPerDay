using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.WebMVC.Models;
using UTS.ScheduleSystem.DomainLogic.DataHandler;


namespace UTS.ScheduleSystem.WebMVC.Controllers
{
    public class EditorController : Controller
    {
        private DomainLogic.EditorService editorService = new DomainLogic.EditorService();
        private string currentUser = System.Web.HttpContext.Current.User.Identity.Name;

        // GET: Editor
        public ActionResult Index()
        {
            List<FixedConversationalRule> fixedConversationalRules = editorService.ShowAllFixedConversationalRuleRules();
            List<ConversationalRule> conversationalRules = editorService.ShowAllConversationalRuleRules();
            ViewBag.FixedConversationalRule = fixedConversationalRules;
            ViewBag.ConversationalRule = conversationalRules;
            return CheckCurrentUser();
        }

        // Check the role of current user
        public ActionResult CheckCurrentUser()
        {
            if (currentUser != "" && UserHandler.GetCurrentUserRole(currentUser).Contains("E"))
            {
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        public ActionResult Create()
        {
            return CheckCurrentUser();
        }

        // Add new rule
        [HttpPost]
        public ActionResult Create([Bind(Include = "Input, Output")] Rule rule)
        {
            rule.Status = "Pending";

            if (editorService.IsFixedRuleValid(rule.Input, rule.Output) && !editorService.CheckRepeatingRule(rule.Input))
            {
                editorService.AddNewFCRule(rule.Input.ToLower(), rule.Output.ToLower(), currentUser);
            }
            else if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output) && !editorService.CheckRepeatingRule(rule.Input))
            {
                editorService.AddNewCRule(rule.Input.ToLower(), rule.Output.ToLower(), currentUser);
            }
            else
            {
                // Show error message
                Response.Write("<script>window.alert('Invalid Input or rule already exit. Please check your input.');</script>");
                return Create();
            }
            return RedirectToAction("Index");
        }

        // Edit a rule
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
            return CheckCurrentUser();
        }

        //Edit a fixed rule
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
            return CheckCurrentUser();
        }

        // Save the edited rule into database
        [HttpPost]
        public ActionResult Edit(ConversationalRule rule)
        {
            rule.Status = "Pending";
            rule.Input = rule.Input.ToLower();
            rule.Output = rule.Output.ToLower();
            if (!ConversationalRuleHandler.FindConversationalRuleById(rule.Id + "").RelatedUsersId.Contains(currentUser))
            {
                rule.RelatedUsersId = ConversationalRuleHandler.FindConversationalRuleById(rule.Id + "").RelatedUsersId + " " + currentUser;
            }
            else
            {
                rule.RelatedUsersId = ConversationalRuleHandler.FindConversationalRuleById(rule.Id + "").RelatedUsersId;
            }
                
            if (editorService.IsRuleValid(rule.Input) && editorService.IsRuleValid(rule.Output) && !editorService.CheckRepeatingRule(rule.Input))
            {
                ConversationalRuleHandler.UpdateAConversationalRule(rule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                Response.Write("<script>window.alert('Invalid Input or rule already exit. Please edit again.');</script>");
                return Edit(rule.Id);
            }

        }

        // Save the edited fixed rule into database
        [HttpPost]
        public ActionResult EditFixed(FixedConversationalRule rule)
        {
            rule.Status = "Pending";
            rule.Input = rule.Input.ToLower();
            rule.Output = rule.Output.ToLower();
            string[] relatedUserIdArray = FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id + "").RelatedUsersId.Split(' ');
            if (!relatedUserIdArray[relatedUserIdArray.Length - 1].Equals(currentUser))
            {
                foreach(string x in relatedUserIdArray)
                {
                    if (x.Equals(currentUser))
                    {
                        FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id + "").RelatedUsersId.Replace(currentUser, "");
                    }
                }
                
                rule.RelatedUsersId = FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id + "").RelatedUsersId + " " + currentUser;
                
            }
            else
            {
                rule.RelatedUsersId = FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id + "").RelatedUsersId;
            }
            if((editorService.CheckRepeatingRule(rule.Input)) && FixedConversationalRuleHandler.FindFixedConversationalRuleById(rule.Id+"").Input == rule.Input && editorService.IsFixedRuleValid(rule.Input, rule.Output))
            {
                FixedConversationalRuleHandler.UpdateAFixedConversationalRule(rule);
                return RedirectToAction("Index");
            }else if (editorService.IsFixedRuleValid(rule.Input, rule.Output) && !editorService.CheckRepeatingRule(rule.Input))
            {
                FixedConversationalRuleHandler.UpdateAFixedConversationalRule(rule);
                return RedirectToAction("Index");
            }
            else
            {
                // Show error message
                Response.Write("<script>window.alert('Invalid Input or rule already exit. Please edit again.');</script>");
                return EditFixed(rule.Id);
            }
        }

        // Delete a rule
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
            return CheckCurrentUser();
        }

        // Delete a fixed rule
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
            return CheckCurrentUser();
        }

        // Delete a rule
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

        // Delete a fixed rule
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

        // Run report
        public ActionResult Report()
        {
            List<FixedConversationalRule> currentUserApprovedfcRules = editorService.ShowCurrentUserApprovedRules(currentUser).Item1;
            List<ConversationalRule> currentUserApprovedcRules = editorService.ShowCurrentUserApprovedRules(currentUser).Item2;
            ViewBag.CurrentUserApprovedfcRules = currentUserApprovedfcRules;
            ViewBag.CurrentUserApprovedcRules = currentUserApprovedcRules;
            ViewBag.CurrentUserApprovedRulesCount = editorService.ShowCurrentUserApprovedRulesCount(currentUser);
            ViewBag.CurrentUserRejectedRulesCount = editorService.ShowCurrentUserRejectedRulesCount(currentUser);
            ViewBag.SuccessRate = editorService.ShowCurrentUserSuccessRate(currentUser);
            return CheckCurrentUser();
        }
    }
}