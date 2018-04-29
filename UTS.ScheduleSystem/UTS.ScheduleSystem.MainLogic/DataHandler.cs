using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem.MainLogic
{
    class DataHandler
    {
        ConversationalRuleTableAdapter conversationalRuleTableAdapter;
        FixedConversationalRuleTableAdapter fixedConversationalRuleTableAdapter;
        MealScheduleTableAdapter mealScheduleTableAdapter;
        AspNetUsersTableAdapter aspNetUsersTableAdapter;

        public DataHandler()
        {
            conversationalRuleTableAdapter = new ConversationalRuleTableAdapter();
            fixedConversationalRuleTableAdapter = new FixedConversationalRuleTableAdapter();
            mealScheduleTableAdapter = new MealScheduleTableAdapter();
            aspNetUsersTableAdapter = new AspNetUsersTableAdapter();
        }

        public void AddConversationalRule()
        {

        }

        public void RemoveConversationalRule()
        {

        }

        public void ChangeOnConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            conversationalRuleTableAdapter.UpdateQuery(input, output, relatedUserId, status, ruleId);
        }

        public void ChangeConversationalRuleState(string id, string status)
        {
            conversationalRuleTableAdapter.UpdateRuleStatus(status, id);
        }

        public string FindSingleConversationalRule(string input)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public List<ConversationalRule> FindConversationalRulesAccordingToStatus(Status status)
        {
            List<ConversationalRule> result = new List<ConversationalRule>();
            foreach(var x in conversationalRuleTableAdapter.GetAllCRulesByStatus(status.ToString()).ToList())
            {
                string id = x.Id;
                string input = x.Input;
                string output = x.Output;
                string relatedUserId = x.RelatedUsersId;
                ConversationalRule conversationalRule = new ConversationalRule(id, input, output, relatedUserId, status);
                result.Add(conversationalRule);
            }
            return result;
        }

        public int FindConversationalRuleNum(string status)
        {
            return 0;
        }

        public void AddFixedConversationalRule()
        {

        }

        public void RemoveFixedConversationalRule()
        {

        }

        public void ChangeOnFixedConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            fixedConversationalRuleTableAdapter.UpdateQuery(input, output, relatedUserId, status, ruleId);
        }

        public void ChangeFixedConversationalRuleState(string id, string status)
        {
            fixedConversationalRuleTableAdapter.UpdateRuleStatus(status, id);
        }

        public string FindSingleFixedConversationalRule(string input)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public List<FixedConversationalRule> FindFixedConversationalRulesAccordingToStatus(Status status)
        {
            List<FixedConversationalRule> result = new List<FixedConversationalRule>();
            foreach (var x in fixedConversationalRuleTableAdapter.GetAllFCRulesByStatus(status.ToString()).ToList())
            {
                string id = x.Id;
                string input = x.Input;
                string output = x.Output;
                string relatedUserId = x.RelatedUsersId;
                FixedConversationalRule conversationalRule = new FixedConversationalRule(id, input, output, relatedUserId, status);
                result.Add(conversationalRule);
            }
            return result;
        }

        public void FindFixedConversationalRules()
        {

        }

        public int FindFixedConversationalRuleNum(string status)
        {
            return 0;
        }

        public void AddMealschedule()
        {

        }

        public void RemoveMealschedule()
        {

        }

        public void ChangeOnMealschedule()
        {

        }

        public string FindSingleMealschedule(string inputKeyword, string outputKeyword, string parameter)
        {
            string result;
            try
            {
                result = "";
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public void FindMealschedules()
        {

        }

        public int FindEditorNum()
        {
            return 0;
        }
    }
}
