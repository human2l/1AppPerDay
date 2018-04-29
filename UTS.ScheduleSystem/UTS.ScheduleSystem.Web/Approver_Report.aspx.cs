﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTS.ScheduleSystem.MainLogic;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver_Report : System.Web.UI.Page
    {
        private Controller controller;
        private string approvedRuleNum;
        private string rejectedRuleNum;
        private string successRate;
        //private List<ConversationalRule> conversationalRules;
        //private List<FixedConversationalRule> fixedConversationalRules;
        private List<Rule> approvedList = new List<Rule>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                //LoadRuleList();
                DisplayApprovedRuleList();
                DisplayStatisticsData();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        // Load rule list from database
        //private void LoadRuleList()
        //{
        //    conversationalRules = controller.ConversationalRulesList;
        //    fixedConversationalRules = controller.FixedConversationalRulesList;
        //}

        // Load approved rule list from database and bind with display
        private void DisplayApprovedRuleList()
        {
            approvedList = controller.ApproverService.RequestApprovedRulesList();
            ApprovedRulesDisplayView.DataSource = approvedList;
            ApprovedRulesDisplayView.DataBind();
        }

        // Refresh statistics data from database and refresh on display table
        private void DisplayStatisticsData()
        {
            approvedRuleNum = controller.ApproverService.ApprovedRulesNum().ToString();
            rejectedRuleNum = controller.ApproverService.RejectedRulesNum().ToString();
            successRate = controller.ApproverService.SuccessRate().ToString("0.00%");
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