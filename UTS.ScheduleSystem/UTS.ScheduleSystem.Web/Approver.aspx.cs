using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                Controller controller = (Controller)Session["Controller"];
                List<Rule> pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                readPendingRule(pendingList);
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        private void readPendingRule(List<Rule> pendingList)
        {
            PendingRuleDisplayView.DataSource = pendingList;
            PendingRuleDisplayView.DataBind();
        }

        protected void PassedRulesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Approver_Report");
        }

        protected void EditorDashboardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Approver_Editor_Report");
        }
    }
}