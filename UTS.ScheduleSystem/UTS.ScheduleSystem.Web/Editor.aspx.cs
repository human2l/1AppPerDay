using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTS.ScheduleSystem.Web
{
    public partial class Editor : System.Web.UI.Page
    {
        Controller controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                List<Rule> rulesList = new List<Rule>();
                rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                EditorGridView.DataSource = rulesList;
                EditorGridView.DataBind();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        protected void Add_rule_Click(object sender, EventArgs e)
        {
            if (Input.Text != "" && Output.Text != "")
            {
                if (Session["Controller"] != null)
                {
                    controller = (Controller)Session["Controller"];
                    if(!controller.EditorService.CheckRepeatingRule(Input.Text, controller.FixedConversationalRulesList, controller.ConversationalRulesList))
                    {
                        controller.ConversationalRulesList = controller.EditorService.AddNewCRule(Input.Text, Output.Text, controller.CurrentUser.Id, controller.ConversationalRulesList);
                        List<Rule> rulesList = new List<Rule>();
                        rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                        EditorGridView.DataSource = rulesList;
                        EditorGridView.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Repeating rule" + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error" + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid input" + "');", true);
            }
            
        }

        protected void Add_fixed_rule_Click(object sender, EventArgs e)
        {
            if (Input.Text != "" && Output.Text != "")
            {
                if (Session["Controller"] != null)
                {
                    controller = (Controller)Session["Controller"];
                    if (!controller.EditorService.CheckRepeatingRule(Input.Text, controller.FixedConversationalRulesList, controller.ConversationalRulesList))
                    {
                        controller.FixedConversationalRulesList = controller.EditorService.AddNewFCRule(Input.Text, Output.Text, controller.CurrentUser.Id, controller.FixedConversationalRulesList);
                        List<Rule> rulesList = new List<Rule>();
                        rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                        EditorGridView.DataSource = rulesList;
                        EditorGridView.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Repeating rule" + "');", true);
                    }   
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error" + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid input" + "');", true);
            }
        }

        protected void EditorGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = EditorGridView.DataKeys[e.RowIndex].Value.ToString();
            var fRuleList = controller.FixedConversationalRulesList;
            var cRuleList = controller.ConversationalRulesList;
            controller.EditorService.DeletePendingRule(id, ref fRuleList, ref cRuleList);
            List<Rule> rulesList = new List<Rule>();
            rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            EditorGridView.DataSource = rulesList;
            EditorGridView.DataBind();
        }
    }
}