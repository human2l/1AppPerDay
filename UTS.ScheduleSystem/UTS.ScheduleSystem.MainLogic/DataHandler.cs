using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem.MainLogic
{
    public class DataHandler
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-UTS.ScheduleSystem.Web-20180415025446.mdf;Initial Catalog=aspnet-UTS.ScheduleSystem.Web-20180415025446;Integrated Security=True";
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


        // Conversational rule
        public void AddConversationalRule(Rule rule)
        {
            conversationalRuleTableAdapter.InsertQuery(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
        }

        public void RemoveConversationalRule(string id)
        {
            conversationalRuleTableAdapter.DeleteQuery(id);
        }

        public void RemoveAllConversationalRule()
        {
            conversationalRuleTableAdapter.DeleteAllQuery();
        }

        public void ChangeOnConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            conversationalRuleTableAdapter.UpdateQuery(input, output, relatedUserId, status, ruleId);
        }

        public void ChangeConversationalRuleState(string id, string status)
        {
            conversationalRuleTableAdapter.UpdateRuleStatus(status, id);
        }

        public ConversationalRule FindConversationalRuleById(string id)
        {
            var x = conversationalRuleTableAdapter.FindConversationalRuleById(id).ToList().First();
            ConversationalRule newRule = new ConversationalRule(x.Id, x.Input, x.Output, x.RelatedUsersId, Utils.GetStatus(x.Status));
            return newRule;
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

        public string FindLastConversationalRuleId()
        {
            string result = conversationalRuleTableAdapter.FindLastIdQuery();
            return result;
            //string result = "c1";
            //try
            //{
            //    result = conversationalRuleTableAdapter.FindLastIdQuery();
            //    result = conversationalRuleTableAdapter.FindLastIdQuery();
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        string query = @"SELECT Id FROM ConversationalRule";
            //        var command = new SqlCommand(query, connection);
            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            string currentId = reader.GetString(0);
            //            int id = Convert.ToInt32(currentId.Substring(1));
            //            if (id > Convert.ToInt32(result.Substring(1)))
            //            {
            //                result = currentId;
            //            }

            //        }
            //        command.Dispose();
            //    }
            //}
            //catch
            //{
            //    result = null;
            //}
            //return result;
        }

        public int FindConversationalRuleNum(string status)
        {
            int result = Convert.ToInt32(conversationalRuleTableAdapter.GetRulesCountByStatus(status));
            return result;
        }


        // Fixed conversational rule
        public void AddFixedConversationalRule(Rule rule)
        {
            fixedConversationalRuleTableAdapter.InsertQuery(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
        }

        public void RemoveFixedConversationalRule(string id)
        {
            fixedConversationalRuleTableAdapter.DeleteQuery(id);
        }

        public void RemoveAllFixedConversationalRule()
        {
            fixedConversationalRuleTableAdapter.DeleteAllQuery();
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
                result = fixedConversationalRuleTableAdapter.GetOutput(input);
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public FixedConversationalRule FindFixedConversationalRuleById(string id)
        {
            var x = fixedConversationalRuleTableAdapter.FindFixedConversationalRuleById(id).ToList().First();
            FixedConversationalRule newRule = new FixedConversationalRule(x.Id, x.Input, x.Output, x.RelatedUsersId, Utils.GetStatus(x.Status));
            return newRule;
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

        public string FindLastFixedConversationalRuleId()
        {
            string result = fixedConversationalRuleTableAdapter.FindLastIdQuery();
            return result;
            //string result = "fc1";
            //try
            //{

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        string query = @"SELECT Id FROM FixedConversationalRule";
            //        var command = new SqlCommand(query, connection);
            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            string currentId = reader.GetString(0);
            //            int id = Convert.ToInt32(currentId.Substring(2));
            //            if (id > Convert.ToInt32(result.Substring(2)))
            //            {
            //                result = currentId;
            //            }

            //        }
            //        command.Dispose();
            //    }
            //}
            //catch
            //{
            //    result = null;
            //}
            //return result;
        }

        public int FindFixedConversationalRuleNum(string status)
        {
            int result = Convert.ToInt32(fixedConversationalRuleTableAdapter.GetRulesCountByStatus(status));
            return result;
        }


        // Mealschedule
        public void AddMealschedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditUserId)
        {
            mealScheduleTableAdapter.Insert(id, topic, participants, location, startDate, endDate, lastEditUserId);
        }

        public void DeleteMealschedule(string Id)
        {
            mealScheduleTableAdapter.DeleteQuery(Id);
        }

        public void RemoveAllMealschedule()
        {
            mealScheduleTableAdapter.DeleteAllQuery();
        }

        public void ChangeOnMealschedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditUserId)
        {
            mealScheduleTableAdapter.UpdateQuery(topic, participants, location, startDate, endDate, lastEditUserId, id);
        }

        public string FindSingleMealschedule(string inputKeyword, string outputKeyword, string parameter)
        {
            string result;
            inputKeyword = inputKeyword.Substring(0, 1).ToUpper() + inputKeyword.Substring(1);
            outputKeyword = outputKeyword.Substring(0, 1).ToUpper() + outputKeyword.Substring(1);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"select " + outputKeyword + " from MealSchedule where " + inputKeyword + "='" + parameter + "'";
                    var command = new SqlCommand(query, connection);
                    result = command.ExecuteScalar().ToString();
                    command.Dispose();
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public MealSchedule FindMealScheduleById(string id)
        {
            var x = mealScheduleTableAdapter.FindMealScheduleById(id).ToList().First();
            MealSchedule mealSchedule = new MealSchedule(x.Id, x.Topic, x.Participants, x.Location, x.StartDate, x.EndDate, x.LastEditUserId);
            return mealSchedule;
        }

        public string FindLastMealscheduleId()
        {
            string result = mealScheduleTableAdapter.FindLastIdQuery();
            return result;
            //string result = "ms1";
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        string query = @"select Id from MealSchedule";
            //        var command = new SqlCommand(query, connection);
            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            string currentId = reader.GetString(0);
            //            int id = Convert.ToInt32(currentId.Substring(2));
            //            if (id > Convert.ToInt32(result.Substring(2)))
            //            {
            //                result = currentId;
            //            }
                            
            //        }
            //        command.Dispose();
            //    }
            //}
            //catch
            //{
            //    result = null;
            //}
            //return result;
        }

        // User
        public int FindEditorNum()
        {
            int result = Convert.ToInt32(aspNetUsersTableAdapter.GetUsersCountByRoleEditor());
            return result;
        }

        public List<string> FindEditorsId()
        {
            List<string> editorsId = new List<string>();
            foreach(var editor in aspNetUsersTableAdapter.GetAllEditors().ToList())
            {
                editorsId.Add(editor.Id);
            }
            return editorsId;
        }
    }
}
