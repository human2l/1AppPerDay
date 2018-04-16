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
        private int approvedRuleNum;
        private int rejectedRuleNum;
        private string successRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                Controller controller = (Controller)Session["Controller"];
                approvedRuleNum = controller.ApproverService.ApprovedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                rejectedRuleNum = controller.ApproverService.RejectedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                double _successRate = controller.ApproverService.SuccessRate(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
                successRate = _successRate.ToString("0%");
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        public int ApprovedRuleNum
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

        public int RejectedRuleNum
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