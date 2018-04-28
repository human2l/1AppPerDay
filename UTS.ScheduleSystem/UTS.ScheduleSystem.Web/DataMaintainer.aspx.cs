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
    public partial class DataMaintainer : System.Web.UI.Page
    {
        Controller controller;
        List<MealSchedule> mealScheduleList;//added

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("page load");
            if (Session["Controller"] != null && controller == null)
            {

                controller = (Controller)Session["Controller"];
                //System.Diagnostics.Debug.WriteLine("load:front mealList Count: " + mealScheduleList.Count);

                mealScheduleList = controller.MealScheduleList;//added
                System.Diagnostics.Debug.WriteLine("loadafter:front mealList Count: " + mealScheduleList.Count);
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
            mealScheduleList = controller.DataMaintainerService.DeleteMealSchedule(id, mealScheduleList);
            controller.MealScheduleList = mealScheduleList;
            UpdateGridView();
        }

        protected void DataMaintainerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = (string)DataMaintainerGridView.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("~/Datamaintainer_Edit.aspx?ID=" + id);
            //System.Diagnostics.Debug.WriteLine("RowEditing!");
            //DataMaintainerGridView.EditIndex = e.NewEditIndex;
            //UpdateGridView();
        }

        private void UpdateGridView()
        {
            //DataMaintainerGridView.Columns[0].Visible = false;
            //DataMaintainerGridView.DataSource = controller.MealScheduleList;

            DataMaintainerGridView.DataSource = mealScheduleList;//changed
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
            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", mealScheduleList), topic, participants, location, startDate, endDate, "blahblah");

            mealScheduleList.Add(ms);
            controller.MealScheduleList = mealScheduleList;
            UpdateGridView();

        }

        protected void DataMaintainerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("RowUpdating!");
            //string id = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            //string topic = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[1] as TextBox).Text;
            //string userId = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[2] as TextBox).Text;
            //string participants = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[3] as TextBox).Text;
            //string location = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[4] as TextBox).Text;
            //string startDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[5] as TextBox).Text;
            //string endDate = (DataMaintainerGridView.Rows[e.RowIndex].Cells[0].Controls[6] as TextBox).Text;
            //System.Diagnostics.Debug.WriteLine("RowUpdating! id = " + id + "topic : "+topic);
            //MealSchedule ms = new MealSchedule(id, userId, topic, participants, location, startDate, endDate, "blahblah");
            //controller.MealScheduleList = controller.DataMaintainerService.UpdateMealSchedule(ms, controller.MealScheduleList);
            //DataMaintainerGridView.EditIndex = -1;
            //UpdateGridView();
        }

        protected void DataMaintainerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RowCancelling!");
            DataMaintainerGridView.EditIndex = -1;
            UpdateGridView();
        }

    }
}