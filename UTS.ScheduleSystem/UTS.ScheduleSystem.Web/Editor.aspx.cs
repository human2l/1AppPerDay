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
    public partial class Editor : System.Web.UI.Page
    {
        Controller controller;

        // Initial two rules tables
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                controller = (Controller)Session["Controller"];
                List<Rule> rulesList = new List<Rule>();
                List<FixedConversationalRule> fcRuleList = controller.FixedConversationalRulesList;
                List<ConversationalRule> cRuleList = controller.ConversationalRulesList;
                rulesList = controller.EditorService.ShowAllPendingRules(fcRuleList, cRuleList);
                PendingGridView.DataSource = rulesList;
                PendingGridView.DataBind();
                rulesList = controller.EditorService.ShowAllRejectedRules(fcRuleList, cRuleList);
                RejectedGridView.DataSource = rulesList;
                RejectedGridView.DataBind();
            }
            else
            {
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Redirect("~/");
            }
        }

        // Handle add conversion rule action, check format, check the validation
        protected void Add_rule_Click(object sender, EventArgs e)
        {
            if (Input.Text != "" && Output.Text != "")
            {
                if (Session["Controller"] != null)
                {
                    controller = (Controller)Session["Controller"];
                    List<FixedConversationalRule> fcRuleList = controller.FixedConversationalRulesList;
                    List<ConversationalRule> cRuleList = controller.ConversationalRulesList;

                    // Check validation
                    if (controller.EditorService.IsRuleValid(Input.Text) && controller.EditorService.IsRuleValid(Output.Text))
                    {

                        // Check whether the input is existed or not
                        if (!controller.EditorService.CheckRepeatingRule(Input.Text, fcRuleList, cRuleList))
                        {
                            controller.ConversationalRulesList = controller.EditorService.AddNewCRule(Input.Text, Output.Text, controller.CurrentUser.Id, cRuleList);
                            List<Rule> rulesList = new List<Rule>();
                            rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                            PendingGridView.DataSource = rulesList;
                            PendingGridView.DataBind();
                        }
                        else
                        {
                            // Show error message to the user
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Repeating rule" + "');", true);
                        }
                    }
                    else
                    {
                        // Show error message to the user
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid rule format, use following fomat: { topic }, { participants }, { location }, { startdate }, { enddate }" + "');", true);
                    }
                   
                }
                else
                {
                    // Show error message to the user
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error" + "');", true);
                }
            }
            else
            {
                // Show error message to the user
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid input" + "');", true);
            }
            
        }

        // Handle add fixed conversion rule action, check format, check the validation
        protected void Add_fixed_rule_Click(object sender, EventArgs e)
        {
            if (Input.Text != "" && Output.Text != "")
            {
                if (Session["Controller"] != null)
                {
                    controller = (Controller)Session["Controller"];
                    List<FixedConversationalRule> fcRuleList = controller.FixedConversationalRulesList;
                    List<ConversationalRule> cRuleList = controller.ConversationalRulesList;

                    // Check validation
                    if (controller.EditorService.IsFixedRuleValid(Input.Text, Output.Text))
                    {

                        // Check whether the input is existed or not
                        if (!controller.EditorService.CheckRepeatingRule(Input.Text, fcRuleList, cRuleList))
                        {
                            controller.FixedConversationalRulesList = controller.EditorService.AddNewFCRule(Input.Text, Output.Text, controller.CurrentUser.Id, fcRuleList);
                            List<Rule> rulesList = new List<Rule>();
                            rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                            PendingGridView.DataSource = rulesList;
                            PendingGridView.DataBind();
                        }
                        else
                        {
                            // Show error message to the user
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Repeating rule" + "');", true);
                        }
                    }
                    else
                    {
                        // Show error message to the user
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid rule format, use following fomat: { topic }, { participants }, { location }, { startdate }, { enddate }" + "');", true);
                    }     
                }
                else
                {
                    // Show error message to the user
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error" + "');", true);
                }
            }
            else
            {
                // Show error message to the user
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid input" + "');", true);
            }
        }

        // Handle delete rule action, delete the selected rule from the database and the table
        protected void PendingGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = PendingGridView.DataKeys[e.RowIndex].Value.ToString();
            var lists = controller.EditorService.DeletePendingRule(id, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            controller.FixedConversationalRulesList = lists.Item1;
            controller.ConversationalRulesList = lists.Item2;
            List<Rule> rulesList = new List<Rule>();
            rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            PendingGridView.DataSource = rulesList;
            PendingGridView.DataBind();
        }

        // Handle edit rule action, jump to another page to edit selected rule
        protected void PendingGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = (string)PendingGridView.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("~/Editor_Edit.aspx?ID=" + id);
        }

        // Handle dropdown list click
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "Pending")
            {
                List<Rule> rulesList = new List<Rule>();
                rulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                PendingGridView.Visible = true;
                RejectedGridView.Visible = false;
                PendingGridView.DataSource = rulesList;
                PendingGridView.DataBind();
            }
            else if (DropDownList1.SelectedValue == "Rejected")
            {
                List<Rule> rulesList = new List<Rule>();
                rulesList = controller.EditorService.ShowAllRejectedRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
                PendingGridView.Visible = false;
                RejectedGridView.Visible = true;
                RejectedGridView.DataSource = rulesList;
                RejectedGridView.DataBind();
                
            }
        }
    }
}