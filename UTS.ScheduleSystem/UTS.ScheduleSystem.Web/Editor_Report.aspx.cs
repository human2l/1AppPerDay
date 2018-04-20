using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Editor_Report : System.Web.UI.Page
    {
        Controller controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                List<Rule> rulesList = new List<Rule>();
                rulesList = controller.EditorService.ShowCurrentUserApprovedRules(controller.CurrentUser, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                EditorReportGridView.DataSource = rulesList;
                EditorReportGridView.DataBind();
                Username.Text = controller.CurrentUser.Name + controller.CurrentUser.Id;
                NumberOfApprovedRules.Text = controller.EditorService.ShowCurrentUserApprovedRulesCount(controller.CurrentUser, controller.FixedConversationalRulesList, controller.ConversationalRulesList).ToString();
                NumberOfRejectedRules.Text = controller.EditorService.ShowCurrentUserRejectedRulesCount(controller.CurrentUser, controller.FixedConversationalRulesList, controller.ConversationalRulesList).ToString();
                SuccessRate.Text = controller.EditorService.ShowCurrentUserSuccessRate(controller.CurrentUser, controller.FixedConversationalRulesList, controller.ConversationalRulesList).ToString();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
                
            }
        }
    }
}