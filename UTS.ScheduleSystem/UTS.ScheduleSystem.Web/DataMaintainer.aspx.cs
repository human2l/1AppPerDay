﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class DataMaintainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MealSchedule ms1 = new MealSchedule("id-s","userId-s","topic-s","participants-s","location-s","startDate-s","endDate-s","lastEditUserId-s");
            MealSchedule ms2 = new MealSchedule("222id-s", "userId-s", "topic-s", "participants-s", "location-s", "startDate-s", "endDate-s", "lastEditUserId-s");
            List<MealSchedule> msList = new List<MealSchedule>();
            msList.Add(ms1);
            msList.Add(ms2);
            DataMaintainerGridView.DataSource = msList;
            DataMaintainerGridView.DataBind();
        }
    }
}