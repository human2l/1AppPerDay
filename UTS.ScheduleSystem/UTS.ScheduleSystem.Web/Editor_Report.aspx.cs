using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Editor_Report : System.Web.UI.Page
    {
        Controller controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                List<Rule> rulesList = new List<Rule>();
                rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                EditorReportGridView.DataSource = rulesList;
                EditorReportGridView.DataBind();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }
    }
}