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
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-UTS.ScheduleSystem.Web-20180415025446.mdf;Initial Catalog=aspnet-UTS.ScheduleSystem.Web-20180415025446;Integrated Security=True";
        private DataHandler dataHandler;
        private string answer;

        public ConversationService()
        {
            dataHandler = new DataHandler();
        }

        // Main conversation function take question input as parameter
        public string Conversation(string question)
        {
            string compactedString = Utils.IgnoreWhiteSpace(question);
            compactedString = Utils.RemoveAllMarks(compactedString).ToLower();

            if (!AnswerToFixedRuleConversation(compactedString) && !AnswerToConversation(compactedString))
                answer = "Can not find answer to the question";
            return answer;
        }

        // Answer to fixed rule conversation
        private Boolean AnswerToFixedRuleConversation(string question)
        {
            answer = dataHandler.FindSingleFixedConversationalRule(question);
            Boolean result = (answer == null) ? false : true;
            return result;
        }

        // Answer to unfixed rule conversation
        private Boolean AnswerToConversation(string question)
        {
            try
            {
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
                        if (question.StartsWith(inputSplit[0]) && question.EndsWith(inputSplit[2]))
                        {
                            string output = dataReader["Output"].ToString();
                            string[] outputSplit = SplitRule(output);

                            string parameter = Parameter(question, inputSplit[0], inputSplit[2]);
                            string inputKeyword = inputSplit[1];
                            string outputKeyword = outputSplit[1];
                            string singleValue = FindAnswerFromMealSchedule(inputKeyword, outputKeyword, parameter);
                            if (singleValue != null)
                            {
                                answer = outputSplit[0] + singleValue + outputSplit[2];
                                command.Dispose();
                                return true;
                            }
                        }
                    }
                    command.Dispose();
                }
            }
            catch
            {
                return false;
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

        // Find answer parameter from Mealschedule according to the input parameter
        private string FindAnswerFromMealSchedule(string inputKeyword, string outputKeyword, string parameter)
        {
            return dataHandler.FindSingleMealschedule(inputKeyword, outputKeyword, parameter);
        }
    }
}
