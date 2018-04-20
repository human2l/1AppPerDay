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
        Controller controller;
        List<Rule> pendingList = new List<Rule>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
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

        protected void PendingRuleDisplayView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string _ruleId = PendingRuleDisplayView.Rows[index].Cells[0].Text;
            switch (e.CommandName)
            {
                case "Approve":
                    if (_ruleId.StartsWith("c"))
                        controller.ConversationalRulesList = controller.ApproverService.ApproveRuleInConversationalRuleList(_ruleId, controller.ConversationalRulesList);
                    else
                        controller.FixedConversationalRulesList = controller.ApproverService.ApproveRuleInFixedConversationalRuleList(_ruleId, controller.FixedConversationalRulesList);
                    pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                    break;
                case "Reject":
                    if (_ruleId.StartsWith("c"))
                        controller.ConversationalRulesList = controller.ApproverService.RejectRuleInConversationalRuleList(_ruleId, controller.ConversationalRulesList);
                    else
                        controller.FixedConversationalRulesList = controller.ApproverService.RejectRuleInFixedConversationalRuleList(_ruleId, controller.FixedConversationalRulesList);
                    pendingList = controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                    break;
                default:
                    break;
            }
            readPendingRule(pendingList);
        }
    }
}