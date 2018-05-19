using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.MainLogic;

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
                //Reject access if no permission
                if (!controller.CurrentUser.Role.ToString().Contains("E") || Request.QueryString[IdKey] == null)
                {
                    Response.Redirect("~/");
                }
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
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

                    // Get current data
                    Rule rule = controller.EditorService.FindRuleById(currentId);
                    if (rule != null)
                    {
                        InputTextBox.Text = rule.Input;
                        OutputTextBox.Text = rule.Output;
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }

                }
            }

           
            
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (currentId != null && currentId != "")
            {
                // Save Changes
                controller.EditorService.EditPendingRule(controller.CurrentUser.Id, currentId,
                    InputTextBox.Text, OutputTextBox.Text, controller.EditorService.ShowAllFixedConversationalRuleRules(), controller.EditorService.ShowAllConversationalRuleRules());
            }

            // Return to the editor page
            Response.Redirect("~/Editor.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Editor.aspx");
        }
    }
}