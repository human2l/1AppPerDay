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
    class DataHandler
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
        public void AddConversationalRule(string id, string input, string output, string relatedUserId, string status)
        {
            conversationalRuleTableAdapter.InsertQuery(id, input, output, relatedUserId, status);
        }

        public void RemoveConversationalRule(string id)
        {
            conversationalRuleTableAdapter.DeleteQuery(id);
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

        public string FindLastConversationalRuleId()
        {
            string result = "c1";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT Id FROM ConversationalRule";
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string currentId = reader.GetString(0);
                        int id = Convert.ToInt32(currentId.Substring(1));
                        if (id > Convert.ToInt32(result.Substring(1)))
                        {
                            result = currentId;
                        }

                    }
                    command.Dispose();
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public int FindConversationalRuleNum(string status)
        {
            int result = Convert.ToInt32(conversationalRuleTableAdapter.GetRulesCountByStatus(status));
            return result;
        }


        // Fixed conversational rule
        public void AddFixedConversationalRule(string id, string input, string output, string relatedUserId, string status)
        {
            fixedConversationalRuleTableAdapter.InsertQuery(id, input, output, relatedUserId, status);
        }

        public void RemoveFixedConversationalRule(string id)
        {
            fixedConversationalRuleTableAdapter.DeleteQuery(id);
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

        public string FindLastFixedConversationalRuleId()
        {
            string result = "fc1";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT Id FROM FixedConversationalRule";
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string currentId = reader.GetString(0);
                        int id = Convert.ToInt32(currentId.Substring(2));
                        if (id > Convert.ToInt32(result.Substring(2)))
                        {
                            result = currentId;
                        }

                    }
                    command.Dispose();
                }
            }
            catch
            {
                result = null;
            }
            return result;
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

        public List<MealSchedule> FindMealSchedules()
        {
            List<MealSchedule> result = new List<MealSchedule>();
            foreach (var x in mealScheduleTableAdapter.GetData().ToList())
            {
                string id = x.Id;
                string topic = x.Topic;
                string participants = x.Participants;
                string location = x.Location;
                string startDate = x.StartDate;
                string endDate = x.EndDate;
                string lastEditorUserId = x.LastEditUserId;
                MealSchedule mealSchedule = new MealSchedule(id, topic, participants, location, startDate, endDate, lastEditorUserId);
                result.Add(mealSchedule);
            }
            return result;
        }

        public string FindLastMealscheduleId()
        {
            string result = "ms1";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"select Id from MealSchedule";
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string currentId = reader.GetString(0);
                        int id = Convert.ToInt32(currentId.Substring(2));
                        if (id > Convert.ToInt32(result.Substring(2)))
                        {
                            result = currentId;
                        }
                            
                    }
                    command.Dispose();
                }
            }
            catch
            {
                result = null;
            }
            return result;
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

        public List<User> FindEditors()
        {
            List<User> editors = new List<User>();
            foreach(var editor in aspNetUsersTableAdapter.GetAllEditors().ToList())
            {
                string id = editor.Id;
                string name = editor.UserName;
                string password = editor.PasswordHash;
                string email = editor.Email;
                string _role = editor.Role;
                Role role = (_role.Equals(Role.A.ToString())) ? Role.A :
                    (_role.Equals(Role.DM.ToString())) ? Role.DM :
                    (_role.Equals(Role.DMnA.ToString())) ? Role.DMnA :
                    (_role.Equals(Role.DMnE.ToString())) ? Role.DMnE :
                    (_role.Equals(Role.DMnEnA.ToString())) ? Role.DMnEnA :
                    (_role.Equals(Role.E.ToString())) ? Role.E :
                    (_role.Equals(Role.EnA.ToString())) ? Role.EnA : Role.None;
                User newEditor = new User(id, name, password, email, role);
                editors.Add(newEditor);
            }
            return editors;
        }
    }
}
