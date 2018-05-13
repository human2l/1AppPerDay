using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic.DatabaseHandler
{
    public class FixedConversationalRuleHandler
    {
        // Add a fixed conversational rule into database
        public static void AddFixedConversationalRule(Rule rule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (FixedConversationalRule)rule;
                context.FixedConversationalRules.Add(fCRule);
                context.SaveChanges();
            }
        }

        // Delete a fixed conversational rule from database by Id
        public static void RemoveFixedConversationalRule(string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                              where FixedConversationalRule.Id == ruleId
                              select FixedConversationalRule).First();
                context.FixedConversationalRules.Remove(fCRule);
                context.SaveChanges();
            }
        }

        // Update a fixed conversational rule by Id
        public static void UpdateAFixedConversationalRule(string input, string output, string relatedUserId, string status, string ruleId)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                              where FixedConversationalRule.Id == ruleId
                              select FixedConversationalRule).First();
                fCRule.Input = input;
                fCRule.Output = output;
                fCRule.RelatedUsersId = relatedUserId;
                fCRule.Status = Utils.GetStatus(status);
                context.SaveChanges();
            }
        }

        // Find a fixed conversational rule by Id
        public static FixedConversationalRule FindFixedConversationalRuleById(string ruleId)
        {
            FixedConversationalRule fixedConversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    fixedConversationalRule = (from FixedConversationalRule
                                                in context.FixedConversationalRules
                                               where FixedConversationalRule.Id == ruleId
                                               select FixedConversationalRule).First();
                }
            }
            catch
            {
                fixedConversationalRule = null;
            }
            return fixedConversationalRule;
        }

        // Find all fixed conversational rules from database
        public static List<FixedConversationalRule> FindAllFixedConversationalRules()
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
            }
            catch
            {
                fixedConversationalRules = null;
            }
            return fixedConversationalRules;
        }
    }
}
