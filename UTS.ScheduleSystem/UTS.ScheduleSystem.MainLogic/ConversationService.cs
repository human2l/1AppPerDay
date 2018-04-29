using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem.MainLogic
{
    public class ConversationService
    {
        //private string[] keywords = { "topic", "participants", "location", "startdate", "enddate" };
        private RuleAdapter ruleAdapter;
        private MealScheduleAdapter mealScheduleAdapter;
        private string answer;

        public ConversationService()
        {
            ruleAdapter = new RuleAdapter();
            mealScheduleAdapter = new MealScheduleAdapter();
        }

        public string Conversation(string question)
        {
            if (!AnswerToFixedRuleConversation(question) && !AnswerToConversation(question))
                answer = "Can not find answer to the question";
            return answer;
        }

        private Boolean AnswerToFixedRuleConversation(string question)
        {
            var adapter = new FixedConversationalRuleTableAdapter();
            var set = adapter.GetData();
            foreach(DataRow row in set.Rows)
            {
                if (row[1].ToString().Equals(question))
                {
                    answer = row[2].ToString();
                    adapter.Dispose();
                    return true;
                }   
            }
            return false;
        }

        private Boolean AnswerToConversation(string question)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"select Input, Output from ConversationalRule where Status='Approved'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string input = dataReader["Input"].ToString();
                    string[] inputSplit = SplitRule(input);
                    Boolean first = question.StartsWith(inputSplit[0]);
                    Boolean second = question.EndsWith(inputSplit[2]);
                    if (question.StartsWith(inputSplit[0]) && question.EndsWith(inputSplit[2]))
                    {
                        string output = dataReader["Output"].ToString();
                        string[] outputSplit = SplitRule(output);

                        string parameter = Parameter(question, inputSplit[0], inputSplit[2]);
                        string inputKeyword = inputSplit[1];
                        string outputKeyword = outputSplit[1];
                        answer = outputSplit[0] +
                            FindAnswerFromMealSchedule(inputKeyword, outputKeyword, parameter) +
                            outputSplit[2];
                        return true;
                    }
                }
            }

                //var set = adapter.GetData();
                //foreach (DataRow row in set.Rows)
                //{
                //    string input = row[1].ToString();
                //    string[] inputSplit = SplitRule(input);

                //    if (question.Contains(inputSplit[0]) && question.Contains(inputSplit[2]))
                //    {
                //        string output = row[2].ToString();
                //        string[] outputSplit = SplitRule(output);

                //        string parameter = Parameter(question, inputSplit[0], inputSplit[2]);
                //        string inputKeyword = inputSplit[1];
                //        string outputKeyword = outputSplit[1];
                //        answer = FindAnswerFromMealSchedule(inputKeyword, outputKeyword, parameter);
                //    }                 
                //}
            return false;
        }

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

        private string Parameter(string question, string leftString, string rightString)
        {
            string result = question;
            if(leftString != "") result = result.Replace(leftString, "");
            if(rightString != "") result = result.Replace(rightString, "");
            return result;
        }

        private string FindAnswerFromMealSchedule(string inputKeyword, string outputKeyword, string parameter)
        {
            return mealScheduleAdapter.FindSingleValue(inputKeyword, parameter, outputKeyword);
        }
    }
}
