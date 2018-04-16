﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Approver_Editor_Report : System.Web.UI.Page
    {
        Controller controller;
        private List<User> _editorList = new List<User>();
        private User currentEditor; 

        private string editorId;
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
                readEditorList(controller);
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        private void readEditorList(Controller controller)
        {
            _editorList = controller.ApproverService.RequestEditorList(controller.UserList);
            foreach (User user in _editorList)
            {
                editorList.Items.Add(user.Id + "_" + user.Name);
            }
        }

        protected void editorList_Click(object sender, BulletedListEventArgs e)
        {
            ListItem editor = editorList.Items[e.Index];
            editorId = (editor.Text.Split('_'))[0];
            foreach(User user in _editorList)
            {
                if (user.Id.Equals(editorId))
                {
                    currentEditor = user;
                    break;
                } 
            }
            editorUsername = currentEditor.Name;
            editorApprovedRuleNum = controller.ApproverService.UserRelatedApprovedRulesNum(currentEditor, controller.ConversationalRulesList, controller.FixedConversationalRulesList).ToString();
            editorRejectedRuleNum = controller.ApproverService.UserRelatedRejectedRulesNum(currentEditor, controller.ConversationalRulesList, controller.FixedConversationalRulesList).ToString();
            editorPendingRuleNum = controller.ApproverService.UserRelatedPendingRulesNum(currentEditor, controller.ConversationalRulesList, controller.FixedConversationalRulesList).ToString();
            editorSuccessRate = controller.ApproverService.UserSuccessRate(currentEditor, controller.ConversationalRulesList, controller.FixedConversationalRulesList).ToString();
            overallSuccessRate = controller.ApproverService.OverallAveSuccessRate(_editorList, controller.ConversationalRulesList, controller.FixedConversationalRulesList).ToString();
        }

        public string EditorId
        {
            get
            {
                return editorId;
            }

            set
            {
                editorId = value;
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