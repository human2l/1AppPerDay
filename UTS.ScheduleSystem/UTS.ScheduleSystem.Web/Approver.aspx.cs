using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using UTS.ScheduleSystem.MainLogic;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver : System.Web.UI.Page
    {
        private Controller controller;
        private List<ConversationalRule> conversationalRules;
        private List<FixedConversationalRule> fixedConversationalRules;
        private List<Rule> pendingList = new List<Rule>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                LoadRuleList();
                DisplayPendingRuleList();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        // Load rule list from database
        private void LoadRuleList()
        {
            conversationalRules = controller.ConversationalRulesList;
            fixedConversationalRules = controller.FixedConversationalRulesList;
        }

        // Pass updated data to database
        private void UpdateDatabase()
        {
            controller.ConversationalRulesList = conversationalRules;
            controller.FixedConversationalRulesList = fixedConversationalRules;
        }

        // Load pending rule list from database and bind with display
        private void DisplayPendingRuleList()
        {
            pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            PendingRuleDisplayView.DataSource = pendingList;
            PendingRuleDisplayView.DataBind();
        }

        // Redirect to approver report page on button clicked
        protected void PassedRulesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Approver_Report");
        }

        // Redirect to editor report page on button clicked
        protected void EditorDashboardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Approver_Editor_Report");
        }

        // Row command function on click of "Approve" & "Reject"
        protected void PendingRuleDisplayView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string _ruleId = PendingRuleDisplayView.Rows[index].Cells[0].Text;
            switch (e.CommandName)
            {
                case "Approve":
                    controller.ApproverService.ApproveRule(_ruleId, ref conversationalRules, ref fixedConversationalRules);
                    UpdateDatabase();
                    break;
                case "Reject":
                    controller.ApproverService.RejectRule(_ruleId, ref conversationalRules, ref fixedConversationalRules);
                    UpdateDatabase();
                    break;
                default:
                    break;
            }
            DisplayPendingRuleList();
        }
    }
}