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
        protected void Page_Load(object sender, EventArgs e)
        {
            ConversationalRule cRule1 = new ConversationalRule("c001", "When will I have meal with {p1}", "It's {p1}", "u001 u002", Status.Pending);
            ConversationalRule cRule2 = new ConversationalRule("c002", "Who will I have meal with on {p1}", "It's {p1}", "u001 u002", Status.Approved);
            ConversationalRule cRule3 = new ConversationalRule("c003", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            FixedConversationalRule cFRule1 = new FixedConversationalRule("fc001", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            FixedConversationalRule cFRule2 = new FixedConversationalRule("fc002", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
            FixedConversationalRule cFRule3 = new FixedConversationalRule("fc003", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            List<ConversationalRule> cRulesList = new List<ConversationalRule>();
            List<FixedConversationalRule> fCRuleLists = new List<FixedConversationalRule>();
            List<Rule> rulesList = new List<Rule>();
            cRulesList.Add(cRule1);
            cRulesList.Add(cRule2);
            cRulesList.Add(cRule3);
            fCRuleLists.Add(cFRule1);
            fCRuleLists.Add(cFRule2);
            fCRuleLists.Add(cFRule3);
            //rulesList = c.EditorService.ShowAllPendingRules(fCRuleLists, cRulesList);
            EditorReportGridView.DataSource = cRulesList;
            EditorReportGridView.DataBind();
        }
    }
}