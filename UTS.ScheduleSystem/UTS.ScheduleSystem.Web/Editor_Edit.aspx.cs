﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Editor_Edit : System.Web.UI.Page
    {
        public const string IdKey = "ID";
        string currentId;
        Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
            }

            if (IsPostBack)
            {
                // Retrieve the ID from the view state

                currentId = (string)ViewState[IdKey];
            }
            else
            {
                //Retrieve the ID from the query string

                string id = Request.QueryString[IdKey];
                if (id != null && id != "")
                {
                    // Edit Mode
                    ModeLabel.Text = "Edit";

                    // Save the ID for later
                    ViewState[IdKey] = id;
                    currentId = id;

                    // Retrieve the item we're editing
                    //var rule = (Rule)GlobalState.AddressBook.SearchById(id);
                    //InputTextBox.Text = rule.Input;
                    //OutputTextBox.Text = rule.Outputput;
                }
                else
                {

                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (currentId != null && currentId != "")
            {
                // Edit
                // Find the Contact
                // Save Changes
                var fcRuleList = controller.FixedConversationalRulesList;
                var cRuleList = controller.ConversationalRulesList;
                controller.EditorService.EditPendingRule(currentId, InputTextBox.Text, OutputTextBox.Text, ref fcRuleList, ref cRuleList);
            }
            else
            {
               
            }

            // Return to the list page
            Response.Redirect("~/Editor.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Editor.aspx");
        }
    }
}