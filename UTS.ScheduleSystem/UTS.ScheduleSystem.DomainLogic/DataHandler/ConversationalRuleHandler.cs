using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.DataHandler
{
    public class ConversationalRuleHandler
    {
        // Add a conversational rule into database
        public static void AddConversationalRule(Rule rule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var conversationalRule = (ConversationalRule)rule;
                context.ConversationalRules.Add(conversationalRule);
                context.SaveChanges();
            }
        }

        // Delete a conversational rule from database by Id
        public static void RemoveConversationalRule(string ruleId)
        {
            int intId = int.Parse(ruleId);
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == intId
                             select ConversationalRule).First();
                context.ConversationalRules.Remove(cRule);
                context.SaveChanges();
            }
        }

        // Update a conversational rule by Id
        public static void UpdateAConversationalRule(ConversationalRule conversationalRule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == conversationalRule.Id
                             select ConversationalRule).First();
                cRule.Input = conversationalRule.Input;
                cRule.Output = conversationalRule.Output;
                cRule.RelatedUsersId = conversationalRule.RelatedUsersId;
                cRule.Status = conversationalRule.Status;
                context.SaveChanges();
            }
        }

        // Find a conversational rule by Id
        public static ConversationalRule FindConversationalRuleById(string ruleId)
        {
            int intId = int.Parse(ruleId);
            ConversationalRule conversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    conversationalRule = (from ConversationalRule
                                            in context.ConversationalRules
                                          where ConversationalRule.Id == intId
                                          select ConversationalRule).First();
                }
            }
            catch
            {
                conversationalRule = null;
            }
            return conversationalRule;
        }

        // Find all conversational rules from database
        public static List<ConversationalRule> FindAllConversationalRules()
        {
            List<ConversationalRule> conversationalRules;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                conversationalRules = (from ConversationalRule
                                       in context.ConversationalRules
                                       select ConversationalRule).ToList();
            }
            return conversationalRules;
        }

        // Find all approved conversational rules from database
        public static List<ConversationalRule> FindAllApprovedConversationalRules()
        {
            List<ConversationalRule> conversationalRules;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                conversationalRules = (from ConversationalRule
                                       in context.ConversationalRules
                                       where ConversationalRule.Status == "Approved"
                                       select ConversationalRule).ToList();
            }
            return conversationalRules;
        }

        // Remove all table rows
        public static void ClearAllConversationalRule()
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ConversationalRule]");
                context.SaveChanges();
            }
        }
    }
}
