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
        List<MealSchedule> mealScheduleList;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("page load");
            if (Session["Controller"] != null && controller == null)
            {

                controller = (Controller)Session["Controller"];
                mealScheduleList = controller.MealScheduleList;
                UpdateGridView();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        //Delet selected row after delete button clicked
        protected void DataMaintainerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = DataMaintainerGridView.DataKeys[e.RowIndex].Value.ToString();
            mealScheduleList = controller.DataMaintainerService.DeleteMealSchedule(id, mealScheduleList);
            controller.MealScheduleList = mealScheduleList;
            UpdateGridView();
        }

        //After edit button clicked, redirect to edit page
        protected void DataMaintainerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = (string)DataMaintainerGridView.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("~/Datamaintainer_Edit.aspx?ID=" + id);
        }

        //reuseful function to refresh gridview data
        private void UpdateGridView()
        {
            DataMaintainerGridView.DataSource = mealScheduleList;
            DataMaintainerGridView.DataBind();
        }

        //Add new MealSchedule after "Add" button clicked
        protected void Button1_Click(object sender, EventArgs e)
        {
            string topic = TopicTB.Text;
            string participants = ParticipantsTB.Text;
            string location = LocationTB.Text;
            string startDate = StartDateTB.Text;
            string endDate = EndDateTB.Text;
            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", mealScheduleList), topic, participants, location, startDate, endDate, "blahblah");

            mealScheduleList.Add(ms);
            controller.MealScheduleList = mealScheduleList;
            UpdateGridView();

        }
    }
}