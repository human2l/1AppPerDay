using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class DataMaintainer : System.Web.UI.Page
    {
        Controller controller;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Controller"] != null && controller == null)
            {
                
                controller = (Controller)Session["Controller"];
                UpdateGridView();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }

            
        }

        protected void DataMaintainerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = DataMaintainerGridView.DataKeys[e.RowIndex].Value.ToString();
            controller.MealScheduleList = controller.DataMaintainerService.deleteMealSchedule(id, controller.MealScheduleList);
            UpdateGridView();
        }

        protected void DataMaintainerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataMaintainerGridView.EditIndex = e.NewEditIndex;
            UpdateGridView();
        }

        private void UpdateGridView()
        {
            DataMaintainerGridView.Columns[0].Visible = false;
            DataMaintainerGridView.DataSource = controller.MealScheduleList;
            DataMaintainerGridView.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string topic = TopicTB.Text;
            string userId = UserIdTB.Text;
            string participants = ParticipantsTB.Text;
            string location = LocationTB.Text;
            string startDate = StartDateTB.Text;
            string endDate = EndDateTB.Text;
            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", controller.MealScheduleList), userId, topic, participants, location, startDate, endDate, "blahblah");

            controller.MealScheduleList.Add(ms);
            UpdateGridView();

        }

        protected void DataMaintainerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text.ToString();
            string topic = (DataMaintainerGridView.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text.ToString();
            string userId = (DataMaintainerGridView.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text.ToString();
            string participants = (DataMaintainerGridView.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text.ToString();
            string location = (DataMaintainerGridView.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox).Text.ToString();
            string startDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[5].Controls[0] as TextBox).Text.ToString();
            string endDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[6].Controls[0] as TextBox).Text.ToString();
            MealSchedule ms = new MealSchedule(id, userId, topic, participants, location, startDate, endDate, "blahblah");
            controller.MealScheduleList = controller.DataMaintainerService.updateMealSchedule(ms, controller.MealScheduleList);
            DataMaintainerGridView.EditIndex = -1;
            UpdateGridView();
        }

    }
}