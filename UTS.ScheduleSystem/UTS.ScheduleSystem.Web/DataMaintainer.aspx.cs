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
                List<MealSchedule> mealScheduleList = new List<MealSchedule>();
                //test

                //controller.MealScheduleList.Add(ms1);
                //controller.MealScheduleList.Add(ms2);

                //--test
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
            DataMaintainerGridView.DataBind();
        }

        private void UpdateGridView()
        {
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
            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", controller.MealScheduleList), userId, topic, participants, location, startDate, endDate,"blahblah");
            controller.MealScheduleList.Add(ms);
            UpdateGridView();
        }
    }
}