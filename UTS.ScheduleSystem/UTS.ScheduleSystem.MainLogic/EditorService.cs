using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class EditorService
    {

        public List<FixedConversationalRule> AddNewFCRule(FixedConversationalRule rule, List<FixedConversationalRule> fCRulesList)
        {
            return fCRulesList;
        }

        public List<ConversationalRule> AddNewCRule(ConversationalRule rule, List<ConversationalRule> cRulesList)
        {
            return cRulesList;
        }

        public List<FixedConversationalRule> ShowAllPendingFCRules(List<FixedConversationalRule> fCRulesList)
        {

            return fCRulesList;
        }

        public List<ConversationalRule> ShowAllPendingCRules(List<ConversationalRule> cRulesList)
        {
            return cRulesList;
        }

        public List<FixedConversationalRule> ShowAllRejectewdFCRules(List<FixedConversationalRule> fCRulesList)
        {
            return fCRulesList;
        }

        public List<ConversationalRule> ShowAllRejectedCRules(List<ConversationalRule> cRulesList)
        {
            return cRulesList;
        }

        public void EditPendingFCRule(FixedConversationalRule fCRule)
        {

        }

        public void EditPendingCRule(ConversationalRule cRule)
        {

        }

        public void DeletePendingFCRule(FixedConversationalRule fCRule)
        {

        }

        public void DeletePendingCRule(ConversationalRule cRule)
        {

        }

        public List<FixedConversationalRule> ShowCurrentUserApprovedFCRules(User user)
        {
            return null;
        }

        public List<ConversationalRule> ShowCurrentUserApprovedCRules(User user)
        {
            return null;
        }

        public int ShowCurrentUserApprovedRulesCount(User user)
        {
            int count = 0;
            return count;
        }

        public int ShowCurrentUserRejectedRulesCount(User user)
        {
            int count = 0;
            return count;
        }

        public double ShowCurrentUserSuccessRate(User user)
        {
            double rate = 0;
            return rate;
        }
    }
}
