using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Controller controller = (Controller)Session["Controller"];
            readPendingRule(controller);
            //ConversationalRule cRule1 = new ConversationalRule("c001", "When will I have meal with {p1}", "It's {p1}", "u001 u002", Status.Pending);
            //ConversationalRule cRule2 = new ConversationalRule("c002", "Who will I have meal with on {p1}", "It's {p1}", "u001 u002", Status.Approved);
            //ConversationalRule cRule3 = new ConversationalRule("c003", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            //FixedConversationalRule cFRule1 = new FixedConversationalRule("fc001", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            //FixedConversationalRule cFRule2 = new FixedConversationalRule("fc002", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
            //FixedConversationalRule cFRule3 = new FixedConversationalRule("fc003", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.FixedConversationalRulesList.Add(cFRule1);
            //controller.FixedConversationalRulesList.Add(cFRule2);
            //controller.FixedConversationalRulesList.Add(cFRule3);
        }

        private void readPendingRule(Controller controller)
        {
            List<Rule> pendingList = (controller != null) ?
                controller.ApproverService.RequestPendingRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList) :
                null;
            PendingRuleDisplayView.DataSource = pendingList;
            PendingRuleDisplayView.DataBind();
        }
    }
}