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
                List<FixedConversationalRule> fcRuleList = controller.FixedConversationalRulesList;
                List<ConversationalRule> cRuleList = controller.ConversationalRulesList;
                rulesList = controller.EditorService.ShowCurrentUserApprovedRules(controller.CurrentUser, fcRuleList, cRuleList);
                EditorReportGridView.DataSource = rulesList;
                EditorReportGridView.DataBind();
                Username.Text = controller.CurrentUser.Name;
                NumberOfApprovedRules.Text = controller.EditorService.ShowCurrentUserApprovedRulesCount(controller.CurrentUser, fcRuleList, cRuleList).ToString();
                NumberOfRejectedRules.Text = controller.EditorService.ShowCurrentUserRejectedRulesCount(controller.CurrentUser, fcRuleList, cRuleList).ToString();
                SuccessRate.Text = controller.EditorService.ShowCurrentUserSuccessRate(controller.CurrentUser, fcRuleList, cRuleList).ToString() + "%";
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
                
            }
        }
    }
}