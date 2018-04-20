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
            System.Diagnostics.Debug.WriteLine("RowEditing!");

            DataMaintainerGridView.EditIndex = e.NewEditIndex;
            UpdateGridView();
        }

        private void UpdateGridView()
        {
            //DataMaintainerGridView.Columns[0].Visible = false;
            DataMaintainerGridView.DataSource = controller.MealScheduleList;
            DataMaintainerGridView.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ButtonClick!");

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
            System.Diagnostics.Debug.WriteLine("RowUpdating!");
            string id = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string topic = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[1] as TextBox).Text;
            string userId = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[2] as TextBox).Text;
            string participants = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[3] as TextBox).Text;
            string location = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[4] as TextBox).Text;
            string startDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[5] as TextBox).Text;
            string endDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[6] as TextBox).Text;
            System.Diagnostics.Debug.WriteLine("RowUpdating! id = " + id + "topic : "+topic);

            MealSchedule ms = new MealSchedule(id, userId, topic, participants, location, startDate, endDate, "blahblah");
            controller.MealScheduleList = controller.DataMaintainerService.updateMealSchedule(ms, controller.MealScheduleList);
            DataMaintainerGridView.EditIndex = -1;
            UpdateGridView();
        }

        protected void DataMaintainerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RowCancelling!");
            DataMaintainerGridView.EditIndex = -1;
            UpdateGridView();
        }

    }
}