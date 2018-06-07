using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.DataHandler
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
            int intId = int.Parse(ruleId);
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                              where FixedConversationalRule.Id == intId
                              select FixedConversationalRule).First();
                context.FixedConversationalRules.Remove(fCRule);
                context.SaveChanges();
            }
        }

        // Update a fixed conversational rule by Id
        public static void UpdateAFixedConversationalRule(FixedConversationalRule fixedConversationalRule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var fCRule = (from FixedConversationalRule
                             in context.FixedConversationalRules
                              where FixedConversationalRule.Id == fixedConversationalRule.Id
                              select FixedConversationalRule).First();
                fCRule.Input = fixedConversationalRule.Input;
                fCRule.Output = fixedConversationalRule.Output;
                fCRule.RelatedUsersId = fixedConversationalRule.RelatedUsersId;
                fCRule.Status = fixedConversationalRule.Status;
                context.SaveChanges();
            }
        }

        // Find a fixed conversational rule by Id
        public static FixedConversationalRule FindFixedConversationalRuleById(string ruleId)
        {
            int intId = int.Parse(ruleId);
            FixedConversationalRule fixedConversationalRule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    fixedConversationalRule = (from FixedConversationalRule
                                                in context.FixedConversationalRules
                                               where FixedConversationalRule.Id == intId
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

        // Find all approved fixed conversational rules from database
        public static List<FixedConversationalRule> FindAllApprovedFixedConversationalRules()
        {
            List<FixedConversationalRule> fixedConversationalRules;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    fixedConversationalRules = (from FixedConversationalRule
                                                in context.FixedConversationalRules
                                                where FixedConversationalRule.Status == "Approved"
                                                select FixedConversationalRule).ToList();
                }
            }
            catch
            {
                fixedConversationalRules = null;
            }
            return fixedConversationalRules;
        }

        // Remove all table rows
        public static void ClearAllFixedConversationalRule()
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [FixedConversationalRule]");
                context.SaveChanges();
            }
        }
    }
}
