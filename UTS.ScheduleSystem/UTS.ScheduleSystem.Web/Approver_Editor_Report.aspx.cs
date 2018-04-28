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
    public partial class Approver_Editor_Report : System.Web.UI.Page
    {
        private Controller controller;
        private List<ConversationalRule> conversationalRules;
        private List<FixedConversationalRule> fixedConversationalRules;
        private List<User> _editorList = new List<User>();
        private User currentEditor; 

        private string editorUsername;
        private string editorApprovedRuleNum;
        private string editorRejectedRuleNum;
        private string editorPendingRuleNum;
        private string editorSuccessRate;
        private string overallSuccessRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                LoadRuleList();
                DisplayEditorList(controller);
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

        // Load editor list from database and bind with display
        private void DisplayEditorList(Controller controller)
        {
            _editorList = controller.ApproverService.RequestEditorList(controller.UserList);
            editorList.DataSource = _editorList;
            editorList.DataBind();
        }

        // Refresh statistics data from database and refresh on display table
        private void DisplayStatisticsData()
        {
            editorUsername = currentEditor.Name;
            editorApprovedRuleNum = controller.ApproverService.UserRelatedApprovedRulesNum(currentEditor, conversationalRules, fixedConversationalRules).ToString();
            editorRejectedRuleNum = controller.ApproverService.UserRelatedRejectedRulesNum(currentEditor, conversationalRules, fixedConversationalRules).ToString();
            editorPendingRuleNum = controller.ApproverService.UserRelatedPendingRulesNum(currentEditor, conversationalRules, fixedConversationalRules).ToString();
            editorSuccessRate = controller.ApproverService.UserSuccessRate(currentEditor, conversationalRules, fixedConversationalRules).ToString("0.00%");
            overallSuccessRate = controller.ApproverService.OverallAveSuccessRate(_editorList, conversationalRules, fixedConversationalRules).ToString("0.00%");
        }

        // Recognize the on selected row editor and save into on focus user
        private void RecognizeUser(string editorId)
        {
            foreach (User user in _editorList)
            {
                if (user.Id.Equals(editorId))
                {
                    currentEditor = user;
                    break;
                }
            }
        }

        // Row command function on click of "Check"
        protected void EditorList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string editorId = editorList.Rows[index].Cells[0].Text;
            LoadRuleList();
            RecognizeUser(editorId);
            switch (e.CommandName)
            {
                case "Check":
                    DisplayStatisticsData();
                    break;
                default:
                    break;
            }
        }

        public string EditorUsername
        {
            get
            {
                return editorUsername;
            }

            set
            {
                editorUsername = value;
            }
        }

        public string EditorApprovedRuleNum
        {
            get
            {
                return editorApprovedRuleNum;
            }

            set
            {
                editorApprovedRuleNum = value;
            }
        }

        public string EditorRejectedRuleNum
        {
            get
            {
                return editorRejectedRuleNum;
            }

            set
            {
                editorRejectedRuleNum = value;
            }
        }

        public string EditorPendingRuleNum
        {
            get
            {
                return editorPendingRuleNum;
            }

            set
            {
                editorPendingRuleNum = value;
            }
        }

        public string EditorSuccessRate
        {
            get
            {
                return editorSuccessRate;
            }

            set
            {
                editorSuccessRate = value;
            }
        }

        public string OverallSuccessRate
        {
            get
            {
                return overallSuccessRate;
            }

            set
            {
                overallSuccessRate = value;
            }
        }

        
    }
}