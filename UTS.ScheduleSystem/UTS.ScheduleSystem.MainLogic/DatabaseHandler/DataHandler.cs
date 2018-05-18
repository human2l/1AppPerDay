using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using UTS.ScheduleSystem.Data.ScheduleSystemDataSetsTableAdapters;

namespace UTS.ScheduleSystem.MainLogic.DatabaseHandler
{
    public class DataHandler
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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


        // Add a conversational rule into database
        public void AddConversationalRule(Rule rule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var conversationalRule = (ConversationalRule)rule;
                context.ConversationalRules.Add(conversationalRule);
                context.SaveChanges();                
            }
                //conversationalRuleTableAdapter.InsertQuery(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
        }

        // Delete a conversational rule from database by Id
        public void RemoveConversationalRule(string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule 
                             in context.ConversationalRules
                             where ConversationalRule.Id == int.Parse(ruleId)
                             select ConversationalRule).First();
                context.ConversationalRules.Remove(cRule);
                context.SaveChanges();
            }
            //conversationalRuleTableAdapter.DeleteQuery(id);
        }

        // Delete all conversational rules from database
        public void RemoveAllConversationalRule()
        {
            //using (ScheduleSystemContext context = new ScheduleSystemContext())
            //{
            //    context.ConversationalRules
            //    context.SaveChanges();
            //}
            //conversationalRuleTableAdapter.DeleteAllQuery();
        }

        // Update a conversational rule by Id
        public void UpdateAConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == int.Parse(ruleId)
                             select ConversationalRule).First();
                cRule.Input = input;
                cRule.Output = output;
                cRule.RelatedUsersId = relatedUserId;
                cRule.Status = status;
                context.SaveChanges();
            }
            //conversationalRuleTableAdapter.UpdateQuery(input, output, relatedUserId, status, ruleId);
        }

        // Update only status of a conversational rule by Id
        public void ChangeConversationalRuleState(string ruleId, string status)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == int.Parse(ruleId)
                             select ConversationalRule).First();
                cRule.Status = status;
                context.SaveChanges();
            }
            //conversationalRuleTableAdapter.UpdateRuleStatus(status, id);
        }

        // Find a conversational rule by Id
        public ConversationalRule FindConversationalRuleById(string ruleId)
        {
            ConversationalRule conversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    conversationalRule = (from ConversationalRule
                                 in context.ConversationalRules
                                 where ConversationalRule.Id == int.Parse(ruleId)
                                 select ConversationalRule).First();
                }
                //var x = conversationalRuleTableAdapter.FindConversationalRuleById(id).ToList().First();
                //newRule = new ConversationalRule(x.Id, x.Input, x.Output, x.RelatedUsersId, Utils.GetStatus(x.Status));
            }
            catch
            {
                conversationalRule = null;
            }
            return conversationalRule;
        }

        // Find all conversational rules from database
        public List<ConversationalRule> FindAllConversationalRule()
        {
            List<ConversationalRule> conversationalRules;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    conversationalRules = (from ConversationalRule
                                           in context.ConversationalRules
                                           select ConversationalRule).ToList();
                }
            }
            catch
            {
                conversationalRules = null;
            }
            return conversationalRules;
        }

        // Find conversational rules by status
        public List<ConversationalRule> FindConversationalRulesAccordingToStatus(Status status)
        {
            List<ConversationalRule> result;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    result = (from ConversationalRule
                                 in context.ConversationalRules
                              select ConversationalRule).ToList();
                }
            }
            catch
            {
                result = null;
            }
            
            //foreach (var x in conversationalRuleTableAdapter.GetAllCRulesByStatus(status.ToString()).ToList())
            //{
            //    string id = x.Id;
            //    string input = x.Input;
            //    string output = x.Output;
            //    string relatedUserId = x.RelatedUsersId;
            //    ConversationalRule conversationalRule = new ConversationalRule(id, input, output, relatedUserId, status);
            //    result.Add(conversationalRule);
            //}
            return result;
        }

        // Find last added conversational rule Id 
        public int FindLastConversationalRuleId()
        {
            int result;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                result = context.ConversationalRules.Max(ConversationalRule => ConversationalRule.Id);
            }
            //string result = conversationalRuleTableAdapter.FindLastIdQuery();
            return result;
        }

        // Find number of conversational rules
        public int FindConversationalRuleNum(string status)
        {
            int result = Convert.ToInt32(conversationalRuleTableAdapter.GetRulesCountByStatus(status));
            return result;
        }


        // Add a fixed conversational rule into database
        public void AddFixedConversationalRule(Rule rule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (FixedConversationalRule)rule;
                context.FixedConversationalRules.Add(fCRule);
                context.SaveChanges();
            }
            //fixedConversationalRuleTableAdapter.InsertQuery(rule.Id, rule.Input, rule.Output, rule.RelatedUsersId, rule.Status.ToString());
        }

        // Delete a fixed conversational rule from database by Id
        public void RemoveFixedConversationalRule(string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                             where FixedConversationalRule.Id == int.Parse(ruleId)
                             select FixedConversationalRule).First();
                context.FixedConversationalRules.Remove(fCRule);
                context.SaveChanges();
            }
            //fixedConversationalRuleTableAdapter.DeleteQuery(id);
        }

        // Delete all fixed conversational rules from database
        public void RemoveAllFixedConversationalRule()
        {
            fixedConversationalRuleTableAdapter.DeleteAllQuery();
        }

        // Update a fixed conversational rule by Id
        public void UpdateAFixedConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                             where FixedConversationalRule.Id == int.Parse(ruleId)
                             select FixedConversationalRule).First();
                fCRule.Input = input;
                fCRule.Output = output;
                fCRule.RelatedUsersId = relatedUserId;
                fCRule.Status = status;
                context.SaveChanges();
            }
            //fixedConversationalRuleTableAdapter.UpdateQuery(input, output, relatedUserId, status, ruleId);
        }

        // Update only status of a fixed conversational rule by Id
        public void ChangeFixedConversationalRuleState(string ruleId, string status)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                              where FixedConversationalRule.Id == int.Parse(ruleId)
                              select FixedConversationalRule).First();
                fCRule.Status = status;
                context.SaveChanges();
            }
            //fixedConversationalRuleTableAdapter.UpdateRuleStatus(status, ruleId);
        }

        // Find corresponding output data by input from database
        public string FindSingleFixedConversationalRule(string input)
        {
            string result;
            try
            {
                result = fixedConversationalRuleTableAdapter.GetOutput(input).ToString();
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Find a fixed conversational rule by Id
        public FixedConversationalRule FindFixedConversationalRuleById(string ruleId)
        {
            FixedConversationalRule fixedConversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    fixedConversationalRule = (from FixedConversationalRule
                                          in context.FixedConversationalRules
                                          where FixedConversationalRule.Id == int.Parse(ruleId)
                                          select FixedConversationalRule).First();
                }
                //var x = fixedConversationalRuleTableAdapter.FindFixedConversationalRuleById(id).ToList().First();
                //newRule = new FixedConversationalRule(x.Id, x.Input, x.Output, x.RelatedUsersId, Utils.GetStatus(x.Status));
            }
            catch
            {
                fixedConversationalRule = null;
            }
            return fixedConversationalRule;
        }

        // Find all fixed conversational rules from database
        public List<FixedConversationalRule> FindAllFixedConversationalRuleById()
        {
            List<FixedConversationalRule> fixedConversationalRules;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    fixedConversationalRules = (from FixedConversationalRule
                                                in context.FixedConversationalRules
                                               select FixedConversationalRule).ToList();
                }
                //var x = fixedConversationalRuleTableAdapter.FindFixedConversationalRuleById(id).ToList().First();
                //newRule = new FixedConversationalRule(x.Id, x.Input, x.Output, x.RelatedUsersId, Utils.GetStatus(x.Status));
            }
            catch
            {
                fixedConversationalRules = null;
            }
            return fixedConversationalRules;
        }

        // Find fixed conversational rules by status
        public List<FixedConversationalRule> FindFixedConversationalRulesAccordingToStatus(Status status)
        {
            List<FixedConversationalRule> result = new List<FixedConversationalRule>();
            foreach (var x in fixedConversationalRuleTableAdapter.GetAllFCRulesByStatus(status.ToString()).ToList())
            {
                string id = x.Id;
                string input = x.Input;
                string output = x.Output;
                string relatedUserId = x.RelatedUsersId;
                FixedConversationalRule conversationalRule = new FixedConversationalRule(id, input, output, relatedUserId, status.ToString());
                result.Add(conversationalRule);
            }
            return result;
        }

        // Find last added fixed conversational rule Id 
        public int FindLastFixedConversationalRuleId()
        {
            int result;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                result = context.FixedConversationalRules.Max(FixedConversationalRule => FixedConversationalRule.Id);
            }
            //string result = fixedConversationalRuleTableAdapter.FindLastIdQuery();
            return result;
        }

        // Find number of fixed conversational rules
        public int FindFixedConversationalRuleNum(string status)
        {
            int result = Convert.ToInt32(fixedConversationalRuleTableAdapter.GetRulesCountByStatus(status));
            return result;
        }


        // Add a mealschedule data into database
        public void AddMealschedule(MealSchedule mealSchedule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                context.MealSchedules.Add(mealSchedule);
                context.SaveChanges();
            }
            //mealScheduleTableAdapter.Insert(mealSchedule.Id, mealSchedule.Topic, mealSchedule.Participants, mealSchedule.Location, mealSchedule.StartDate, mealSchedule.EndDate, mealSchedule.LastEditUserId);
        }

        // Delete a mealschedule data from database
        public void RemoveMealschedule(string Id)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var mealschedule = (from MealSchedule
                                            in context.MealSchedules
                                          where MealSchedule.Id == int.Parse(Id)
                                          select MealSchedule).First();
                context.MealSchedules.Remove(mealschedule);
                context.SaveChanges();
            }
            //mealScheduleTableAdapter.DeleteQuery(Id);
        }

        // Delete all mealschedule data from database
        public void RemoveAllMealschedule()
        {
            mealScheduleTableAdapter.DeleteAllQuery();
        }

        // Update a mealschedule data by Id
        public void UpdateAMealschedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditUserId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var mealschedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Id == int.Parse(id)
                                    select MealSchedule).First();
                mealschedule.Topic = topic;
                mealschedule.Participants = participants;
                mealschedule.Location = location;
                mealschedule.StartDate = startDate;
                mealschedule.EndDate = endDate;
                mealschedule.LastEditUserId = lastEditUserId;
                context.SaveChanges();
            }
            //mealScheduleTableAdapter.UpdateQuery(topic, participants, location, startDate, endDate, lastEditUserId, id);
        }

        // Find a corresponding data of mealschedule output by input
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

        // Find meal sechdule by Id
        public MealSchedule FindMealScheduleById(string id)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Id == int.Parse(id)
                                    select MealSchedule).First();
                }
                //var x = mealScheduleTableAdapter.FindMealScheduleById(id).ToList().First();
                //mealSchedule = new MealSchedule(x.Id, x.Topic, x.Participants, x.Location, x.StartDate, x.EndDate, x.LastEditUserId);
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }

        // Find last added meal schedule Id
        //public string FindLastMealscheduleId()
        //{
        //    string result;
        //    using (ScheduleSystemContext context = new ScheduleSystemContext())
        //    {
        //        result = context.MealSchedules.Max(MealSchedule => MealSchedule.Id);
        //    }
        //    //string result = mealScheduleTableAdapter.FindLastIdQuery();
        //    return result;
        //}

        // Find number of editors
        public int FindEditorNum()
        {
            int result = Convert.ToInt32(aspNetUsersTableAdapter.GetUsersCountByRoleEditor());
            return result;
        }

        // Find all editors id and return as a list
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
