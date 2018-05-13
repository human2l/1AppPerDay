using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic.DatabaseHandler
{
    class ConversationalRuleHandler
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
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == ruleId
                             select ConversationalRule).First();
                context.ConversationalRules.Remove(cRule);
                context.SaveChanges();
            }
        }

        // Update a conversational rule by Id
        public static void UpdateAConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var cRule = (from ConversationalRule
                             in context.ConversationalRules
                             where ConversationalRule.Id == ruleId
                             select ConversationalRule).First();
                cRule.Input = input;
                cRule.Output = output;
                cRule.RelatedUsersId = relatedUserId;
                cRule.Status = Utils.GetStatus(status);
                context.SaveChanges();
            }
        }

        // Find a conversational rule by Id
        public static ConversationalRule FindConversationalRuleById(string ruleId)
        {
            ConversationalRule conversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    conversationalRule = (from ConversationalRule
                                 in context.ConversationalRules
                                          where ConversationalRule.Id == ruleId
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

    }
}
