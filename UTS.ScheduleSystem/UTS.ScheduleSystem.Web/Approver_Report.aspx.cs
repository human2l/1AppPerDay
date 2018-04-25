using Microsoft.AspNet.Identity;
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
        private string approvedRuleNum;
        private string rejectedRuleNum;
        private string successRate;
        private List<Rule> approvedList = new List<Rule>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                Controller controller = (Controller)Session["Controller"];
                List<ConversationalRule> conversationalRules = controller.ConversationalRulesList;
                List<FixedConversationalRule> fixedConversationalRules = controller.FixedConversationalRulesList;
                approvedList = controller.ApproverService.RequestApprovedRulesList(conversationalRules, fixedConversationalRules);
                readApprovedRule();
                approvedRuleNum = controller.ApproverService.ApprovedRulesNum(conversationalRules, fixedConversationalRules).ToString();
                rejectedRuleNum = controller.ApproverService.RejectedRulesNum(conversationalRules, fixedConversationalRules).ToString();
                successRate = controller.ApproverService.SuccessRate(conversationalRules, fixedConversationalRules).ToString("0.00%");
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        private void readApprovedRule()
        {
            ApprovedRulesDisplayView.DataSource = approvedList;
            ApprovedRulesDisplayView.DataBind();
        }

        public string ApprovedRuleNum
        {
            get
            {
                return approvedRuleNum;
            }

            set
            {
                approvedRuleNum = value;
            }
        }

        public string RejectedRuleNum
        {
            get
            {
                return rejectedRuleNum;
            }

            set
            {
                rejectedRuleNum = value;
            }
        }

        public string SuccessRate
        {
            get
            {
                return successRate;
            }

            set
            {
                successRate = value;
            }
        }

    }
}