using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class ApproverService
    {
       
        public ApproverService()
        {

        }

        //if return null display cannot find
        public Rule findRule(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, string id)
        {
            Rule rule = null;
            foreach (Rule ruleX in cRulesList)
            {
                if (ruleX.Id.Equals(id))
                    rule = ruleX;
            }
            foreach (Rule ruleX in fCRulesList)
            {
                if (ruleX.Id.Equals(id))
                    rule = ruleX;
            }
            return rule;
        }

        public List<Rule> traversalList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, Status status)
        {
            List<Rule> newRulesList = new List<Rule>();
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status.Equals(status))
                    newRulesList.Add(rule);
            }
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status.Equals(Status.Pending))
                    newRulesList.Add(rule);
            }
            return newRulesList;
        }

        public List<Rule> requestPendingRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> pRulesList = traversalList(cRulesList, fCRulesList, Status.Pending);
            return pRulesList;
        }

        public List<Rule> requestApprovedRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> apvRulesList = traversalList(cRulesList, fCRulesList, Status.Approved);
            return apvRulesList;
        }

        private List<Rule> requestRejectedRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rjRulesList = traversalList(cRulesList, fCRulesList, Status.Rejected);
            return rjRulesList;
        }

        public string getLastUser(Rule rule)
        {
            string relatedUsersId = rule.RelatedUsersId;
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastUserId = relatedUsersIdString[relatedUsersIdString.Length];
            return lastUserId;
        }

        public void approveRule(Rule rule, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
        {
            for(int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(rule.Id))
                {
                    cRulesList[x].Status = Status.Approved;
                    break;
                } 
            }
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(rule.Id))
                {
                    fCRulesList[x].Status = Status.Approved;
                    break;
                }
            }
        }

        public void rejectRule(Rule rule, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
        {
            for (int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(rule.Id))
                {
                    cRulesList[x].Status = Status.Rejected;
                    break;
                }
            }
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(rule.Id))
                {
                    fCRulesList[x].Status = Status.Rejected;
                    break;
                }
            }
        }

        
        public int approvedRulesNum(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> approvedRulesList = requestApprovedRulesList(cRulesList, fCRulesList);
            return approvedRulesList.Count;
        }

        public int rejectedRulesNum(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rejectedRulesList = requestRejectedRulesList(cRulesList, fCRulesList);
            return rejectedRulesList.Count;
        }

        public int successRate(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            int approved = approvedRulesNum(cRulesList, fCRulesList);
            int rejected = rejectedRulesNum(cRulesList, fCRulesList);
            int rate = approved/(approved+rejected);
            return rate;
        }

        private int countUserRelatedRule(User user, List<Rule> rules)
        {
            int result = 0;
            foreach (Rule rule in rules)
            {
                if (rule.RelatedUsersId.Contains(user.Id))
                    result++;
            }
            return result;
        }

        public int countUserApprovedRule(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, Status status)
        {
            switch(status)
                case Status.Approved:
                    List<Rule> approvedList = requestApprovedRulesList(cRulesList, fCRulesList);
                break;
            case Status.Pending:
                break;
                case 

                return countUserRelatedRule(user, approvedList);
        }

        public int countUserRejectedRule(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rejectedList = requestRejectedRulesList(cRulesList, fCRulesList);
            int result = 0;
            foreach (Rule rule in rejectedList)
            {
                if (rule.RelatedUsersId.Contains(user.Id))
                    result++;
            }
            return result;
        }

        public int countUserPendingRule(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> pendingList = requestPendingRulesList(cRulesList, fCRulesList);
            int result = 0;
            foreach (Rule rule in pendingList)
            {
                if (rule.RelatedUsersId.Contains(user.Id))
                    result++;
            }
            return result;
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
