using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver_Report : System.Web.UI.Page
    {
        public int approvedRuleNum;
        public int rejectedRuleNum;
        public string successRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller controller = (Controller)Session["Controller"];
            approvedRuleNum = controller.ApproverService.ApprovedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            rejectedRuleNum = controller.ApproverService.RejectedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            double _successRate = controller.ApproverService.SuccessRate(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            string successRate = string.Format{ "{0:0%}",percent };
        }
    }
}