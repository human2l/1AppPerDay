using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic
{
    public class ConversationService
    {
        private List<ConversationalRule> conversationalRules;
        private List<FixedConversationalRule> fixedConversationalRules;
        private string questionType;
        private string answerType;
        private string questionKeyword;
        private string answerKeyword;
        private string answer;

        public ConversationService()
        {

        }
        
        // Main conversation function take question input as parameter
        public string Conversation(string question)
        {
            conversationalRules = ConversationalRuleHandler.FindAllApprovedConversationalRules();
            fixedConversationalRules = FixedConversationalRuleHandler.FindAllApprovedFixedConversationalRules();

            string formatedQuestion = ConversationFormat(question);
            if (!AnswerToFixedRuleConversation(formatedQuestion) && !AnswerToConversation(formatedQuestion))
                answer = "Cannot find the answer corresponding to the question.";
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
            foreach (ConversationalRule rule in conversationalRules)
            {
                // Find corresponding rule in unfixed conversational rule list
                string inputLeft = SplitRule(rule.Input)[0];
                questionType = SplitRule(rule.Input)[1];
                string inputRight = SplitRule(rule.Input)[2];
                if(question.StartsWith(inputLeft) && question.EndsWith(inputRight))
                {
                    answerType = SplitRule(rule.Output)[1];
                    //questionKeyword = SplitRule(question)[1];
                    questionKeyword = Parameter(question, inputLeft, inputRight);
                    answerKeyword = ConversationHandler.GetOutputByInput(questionKeyword, questionType, answerType);
                    if(answerKeyword == null)
                        return false;
                    answer = SplitRule(rule.Output)[0] + answerKeyword + SplitRule(rule.Output)[2];
                    return true;
                }
            }
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

        // Format user question input
        public static string ConversationFormat(string question)
        {
            return Utils.IgnoreSpace(Utils.RemoveAllMarks(question)).ToLower();
        }
    }
}
