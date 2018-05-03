using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTS.ScheduleSystem.MainLogic;

namespace UTS.ScheduleSystem.Web
{
    public partial class DataMaintainer_Edit : System.Web.UI.Page
    {
        Controller controller;
        public const string IdKey = "ID";
        string currentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                if (!controller.CurrentUser.Role.ToString().Contains("DM"))
                {
                    Response.Redirect("~/");
                }
                controller = (Controller)Session["Controller"];
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
                controller.DataMaintainerService.EditMealSchedule(currentId, TopicTextBox.Text, ParticipantsTextBox.Text, LocationTextBox.Text, StartDateTextBox.Text, EndDateTextBox.Text, controller.CurrentUser.Id);
            }

            // Return to the list page
            Response.Redirect("~/Datamaintainer.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Datamaintainer.aspx");
        }
    }
}