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

        // Load pending rule list from database and bind with display
        private void DisplayPendingRuleList()
        {
            pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
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

        protected void PendingRuleDisplayView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string _ruleId = PendingRuleDisplayView.Rows[index].Cells[0].Text;
            LoadRuleList();

            switch (e.CommandName)
            {
                case "Approve":
                    if (_ruleId.StartsWith("c"))
                        controller.ConversationalRulesList = controller.ApproverService.ApproveRuleInConversationalRuleList(_ruleId, conversationalRules);
                    else
                        controller.FixedConversationalRulesList = controller.ApproverService.ApproveRuleInFixedConversationalRuleList(_ruleId, fixedConversationalRules);
                    break;
                case "Reject":
                    if (_ruleId.StartsWith("c"))
                        controller.ConversationalRulesList = controller.ApproverService.RejectRuleInConversationalRuleList(_ruleId, conversationalRules);
                    else
                        controller.FixedConversationalRulesList = controller.ApproverService.RejectRuleInFixedConversationalRuleList(_ruleId, fixedConversationalRules);
                    break;
                default:
                    break;
            }
            DisplayPendingRuleList();
        }
    }
}