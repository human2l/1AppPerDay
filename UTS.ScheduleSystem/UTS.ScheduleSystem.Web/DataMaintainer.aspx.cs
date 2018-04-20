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

            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                List<MealSchedule> mealScheduleList = new List<MealSchedule>();
                //test
                MealSchedule ms1 = new MealSchedule("ms001", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
                MealSchedule ms2 = new MealSchedule("ms002", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
                controller.MealScheduleList.Add(ms1);
                controller.MealScheduleList.Add(ms2);

                //--test
                mealScheduleList = controller.MealScheduleList;
                DataMaintainerGridView.DataSource = mealScheduleList;
                DataMaintainerGridView.DataBind();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }


            
            

            //string newID = Utils.CreateIdByType("MealSchedule", msList);

            //Controller controller = (Controller)Session["Controller"];
            ////controller.initialization();
            //if(controller != null)
            //{
            //    TextBox2.Text = "bu shi null!!!"+newID;
            //}
            //else
            //{
            //    TextBox2.Text = "null..."+newID;
            //}

            //Object a = new object();
            
        }

        protected void DataMaintainerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void DataMaintainerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}