using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.MainLogic
{
    public class ConversationService
    {
        private List<ConversationalRule> conversationalRules;
        private List<FixedConversationalRule> fixedConversationalRules;
        private List<MealSchedule> mealSchedules;
        private string answer;

        public ConversationService()
        {

        }

        // Main conversation function take question input as parameter
        public string Conversation(string question)
        {
            conversationalRules = ConversationalRuleHandler.FindAllConversationalRules();
            fixedConversationalRules = FixedConversationalRuleHandler.FindAllFixedConversationalRules();

            string formatedQuestion = Utils.conversationFormat(question);
            if (!AnswerToFixedRuleConversation(formatedQuestion) && !AnswerToConversation(formatedQuestion))
                answer = "Can not find answer to the question";
            return answer;
        }

        // Answer to fixed rule conversation
        private Boolean AnswerToFixedRuleConversation(string question)
        {
            // Traversal fixed conversational rule list 
            foreach (FixedConversationalRule rule in fixedConversationalRules)
            {
                // Find corresponding rule in fixed conversational rule list
                if (rule.Input.Equals(question))
                {
                    answer = rule.Output;
                    return true;
                }
            }
            return false;
        }

        // Answer to unfixed rule conversation
        private Boolean AnswerToConversation(string question)
        {
            return false;
        }

        // Split a rule into parts
        private string[] SplitRule(string str)
        {
            int left = str.IndexOf('{');
            int right = str.IndexOf('}');
            string leftString = str.Substring(0, left);
            string keyword = str.Substring(left + 1, right - left - 1).Trim();
            string rightString = str.Substring(right + 1);
            string[] result = { leftString, keyword, rightString };
            return result;
        }

        // Split reference parameter from question
        private string Parameter(string question, string leftString, string rightString)
        {
            string result = question;
            if(leftString != "") result = result.Replace(leftString, "");
            if(rightString != "") result = result.Replace(rightString, "");
            return result;
        }

        //// Find answer parameter from Mealschedule according to the input parameter
        //private string FindAnswerFromMealSchedule(string inputKeyword, string outputKeyword, string parameter)
        //{
        //    foreach (MealSchedule mealschedule in mealSchedules)
        //    {
        //        Utils.Datatype(inputKeyword)
        //    }
        //}
    }
}
