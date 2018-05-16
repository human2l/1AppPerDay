﻿using Microsoft.AspNet.Identity;
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

                //Reject access if no permission
                if (!controller.CurrentUser.Role.ToString().Contains("E"))
                {
                    Response.Redirect("~/");
                }
                List<Rule> rulesList = new List<Rule>();
                List<FixedConversationalRule> fcRuleList = controller.EditorService.ShowAllFixedConversationalRuleRules();
                List<ConversationalRule> cRuleList = controller.EditorService.ShowAllConversationalRuleRules();
                BindDataToPtable();
                BindDataToRtable();
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
                    List<FixedConversationalRule> fcRuleList = controller.EditorService.ShowAllFixedConversationalRuleRules();
                    List<ConversationalRule> cRuleList = controller.EditorService.ShowAllConversationalRuleRules();

                    // Check validation
                    if (controller.EditorService.IsRuleValid(Input.Text) && controller.EditorService.IsRuleValid(Output.Text))
                    {

                        // Check whether the input is existed or not
                        if (!controller.EditorService.CheckRepeatingRule(Input.Text, fcRuleList, cRuleList))
                        {
                            controller.EditorService.AddNewCRule(Input.Text, Output.Text, controller.CurrentUser.Id);
                            BindDataToPtable();
                        }
                        else
                        {
                            // Show error message to the user
                            RepeatingRuleError();
                        }
                    }
                    else
                    {
                        // Show error message to the user
                        InvalidFormatError();
                    }
                   
                }
                else
                {
                    // Show error message to the user
                    UnknowError();
                }
            }
            else
            {
                // Show error message to the user
                InvalidInputError();
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
                    List<FixedConversationalRule> fcRuleList = controller.EditorService.ShowAllFixedConversationalRuleRules();
                    List<ConversationalRule> cRuleList = controller.EditorService.ShowAllConversationalRuleRules();

                    // Check validation
                    if (controller.EditorService.IsFixedRuleValid(Input.Text, Output.Text))
                    {

                        // Check whether the input is existed or not
                        if (!controller.EditorService.CheckRepeatingRule(Input.Text, fcRuleList, cRuleList))
                        {
                            controller.EditorService.AddNewFCRule(Input.Text, Output.Text, controller.CurrentUser.Id);
                            BindDataToPtable();
                        }
                        else
                        {
                            // Show error message to the user
                            RepeatingRuleError();
                        }
                    }
                    else
                    {
                        // Show error message to the user
                        InvalidFormatError();
                    }     
                }
                else
                {
                    // Show error message to the user
                    UnknowError();
                }
            }
            else
            {
                // Show error message to the user
                InvalidInputError();
            }
        }

        // Handle delete rule action, delete the selected rule from the database and the table
        protected void PendingGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = PendingGridView.DataKeys[e.RowIndex].Value.ToString();
            controller.EditorService.DeletePendingRule(id);

            BindDataToPtable();
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
                BindDataToPtable();
                PendingGridView.Visible = true;
                RejectedGridView.Visible = false;
            }
            else if (DropDownList1.SelectedValue == "Rejected")
            {
                BindDataToRtable();
                PendingGridView.Visible = false;
                RejectedGridView.Visible = true;
            }
        }

        // Bind data to pending rules table
        protected void BindDataToPtable ()
        {
            List<Rule> rulesList = new List<Rule>();
            rulesList = controller.EditorService.ShowAllPendingRules();
            PendingGridView.DataSource = rulesList;
            PendingGridView.DataBind();
        }

        // Bind data to rejected rules table
        protected void BindDataToRtable()
        {
            List<Rule> rulesList = new List<Rule>();
            rulesList = controller.EditorService.ShowAllRejectedRules();
            RejectedGridView.DataSource = rulesList;
            RejectedGridView.DataBind();
        }

        // Shows error message
        protected void InvalidInputError ()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid input" + "');", true);
        }

        // Shows error message
        protected void RepeatingRuleError ()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Repeating rule" + "');", true);
        }

        // Shows error message
        protected void InvalidFormatError ()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Invalid rule format, " +
                "use following fomat: { topic }, { participants }, { location }, { startdate }, { enddate }" + "');", true);
        }

        // Shows error message
        protected void UnknowError ()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unknow error" + "');", true);
        }

    }
}