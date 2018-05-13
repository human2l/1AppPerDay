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

                //Reject access if no permission
                if (!controller.CurrentUser.Role.ToString().Contains("DM"))
                {
                    Response.Redirect("~/");
                }

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
            controller.DataMaintainerService.DeleteMealSchedule(id);
            UpdateGridView();
        }

        //After edit button clicked, redirect to edit page
        protected void DataMaintainerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = (string)DataMaintainerGridView.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("~/DataMaintainer_Edit.aspx?ID=" + id);
            //string id = (string)DataMaintainerGridView.DataKeys[e.NewEditIndex].Value;
            //Session["Topic"] = DataMaintainerGridView.Rows[e.NewEditIndex].Cells[1].Text;
            //Session["Participants"] = DataMaintainerGridView.Rows[e.NewEditIndex].Cells[2].Text;
            //Session["Location"] = DataMaintainerGridView.Rows[e.NewEditIndex].Cells[3].Text;
            //Session["StartDate"] = DataMaintainerGridView.Rows[e.NewEditIndex].Cells[4].Text;
            //Session["EndDate"] = DataMaintainerGridView.Rows[e.NewEditIndex].Cells[5].Text;
            //Response.Redirect("~/Datamaintainer_Edit.aspx?ID=" + id);
        }

        //reuseful function to refresh gridview data
        private void UpdateGridView()
        {
            mealScheduleList = controller.DataMaintainerService.FindAllMealSchedules();
            DataMaintainerGridView.DataSource = mealScheduleList;
            DataMaintainerGridView.DataBind();
        }

        //Add new MealSchedule after "Add" button clicked
        protected void Add_Click(object sender, EventArgs e)
        {
            string topic = TopicTB.Text;
            string participants = ParticipantsTB.Text;
            string location = LocationTB.Text;
            string startDate = StartDateTB.Text;
            string endDate = EndDateTB.Text;
            string lastEditorUserId = controller.CurrentUser.Id;

            controller.DataMaintainerService.AddMealSchedule(topic, participants, location, startDate, endDate, lastEditorUserId);
            UpdateGridView();

        }
    }
}