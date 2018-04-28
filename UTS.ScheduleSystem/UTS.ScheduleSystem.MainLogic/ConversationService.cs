using System;
using System.Collections.Generic;
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
        private string answer;

        public ConversationService()
        {

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
                    answer = row[2].ToString();
            }
            //access to DB and return answer
            adapter.Dispose();
            return true;
        }

        private Boolean AnswerToConversation(string question)
        {
            //var adapter = new ConversationalRuleTableAdapter();
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
            string keyword = str.Substring(left + 1, right - left - 1);
            string rightString = str.Substring(right + 1);
            string[] result = { leftString, keyword, rightString };
            return result;
        }

        private string Parameter(string question, string leftString, string rightString)
        {
            string result = question;
            result.Replace(leftString, "");
            result.Replace(rightString, "");
            return result;
        }

        //private string FindAnswerFromMealSchedule(string inputKeyword, string outputKeyword, string parameter)
        //{
        //    var adapter = new MealScheduleTableAdapter();
        //    var set = adapter.GetData();
        //    DataTable table = set.CopyToDataTable;
        //    DataRow[] dataRow = set.Select(inputKeyword + "='" + parameter + "'");
        //    DataColumn targetColumn;
        //    switch (inputKeyword)
        //    {
        //        case "topic":
        //            targetColumn = set.TopicColumn;
        //            break;
        //        case "participants":
        //            targetColumn = set.ParticipantsColumn;
        //            break;
        //        case "location":
        //            targetColumn = set.LocationColumn;
        //            break;
        //        case "startdate":
        //            targetColumn = set.StartDateColumn;
        //            break;
        //        case "enddate":
        //            targetColumn = set.EndDateColumn;
        //            break;
        //        default:
        //            break;
        //    }
        //    dataRow[0].
        //}
    }
}
