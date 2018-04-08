using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class ApproverService
    {
        private List<ConversationalRule> cRulesList = new List<ConversationalRule>();
        private List<FixedConversationalRule> fCRulesList = new List<FixedConversationalRule>();

        public ApproverService()
        {

        }

        public List<Rule> requestPendingRulesList()
        {
            List<Rule> pRulesList = new List<Rule>();
            foreach(Rule rule in cRulesList)
            {
                if (rule.Status.Equals(Status.Pending))
                    pRulesList.Add(rule);
            }
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status.Equals(Status.Pending))
                    pRulesList.Add(rule);
            }
            return pRulesList;
        }

        public string getLastUser(Rule rule)
        {
            string relatedUsersId = rule.RelatedUsersId;
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastUserId = relatedUsersIdString[relatedUsersIdString.Length];
            return lastUserId;
        }

        public void approveRule(Rule rule)
        {
            rule.Status = Status.Approved;
        }

        public void rejectRule(Rule rule)
        {
            rule.Status = Status.Rejected;
        }

        public List<Rule> requestApprovedRulesList()
        {
            List<Rule> apvRulesList = new List<Rule>();
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status.Equals(Status.Pending))
                    apvRulesList.Add(rule);
            }
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status.Equals(Status.Pending))
                    apvRulesList.Add(rule);
            }
            return apvRulesList;
        }

        public int countApprovedRules()
        {
            int haha = 0;
            return haha;
        }

        public int countRejectedRules()
        {
            int haha = 0;
            return haha;
        }

        public int successRate()
        {
            int haha = 0;
            return haha;
        }

        public int countUserApprovedRule(User user)
        {
            int haha = 0;
            return haha;
        }

        public int countUserRejectedRule(User user)
        {
            int haha = 0;
            return haha;
        }

        public int countUserPendingRule(User user)
        {
            int haha = 0;
            return haha;
        }

        public int userSuccessRate(User user)
        {
            int haha = 0;
            return haha;
        }

        public int overallAveSuccessRate(User user)
        {
            int haha = 0;
            return haha;
        }
    }
}
