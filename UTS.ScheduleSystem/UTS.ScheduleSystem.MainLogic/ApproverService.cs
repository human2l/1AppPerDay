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
        //private Rule findRule(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, string id)
        //{
        //    Rule rule = null;
        //    foreach (Rule ruleX in cRulesList)
        //    {
        //        if (ruleX.Id.Equals(id))
        //            rule = ruleX;
        //    }
        //    foreach (Rule ruleX in fCRulesList)
        //    {
        //        if (ruleX.Id.Equals(id))
        //            rule = ruleX;
        //    }
        //    return rule;
        //}

        private List<Rule> traversalList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, Status status)
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

        private List<Rule> requestRejectedRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rjRulesList = traversalList(cRulesList, fCRulesList, Status.Rejected);
            return rjRulesList;
        }

        private string getLastUser(Rule rule)
        {
            string relatedUsersId = rule.RelatedUsersId;
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastUserId = relatedUsersIdString[relatedUsersIdString.Length];
            return lastUserId;
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


        public void approveRule(string ruleIdid, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
        {
            for(int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(ruleIdid))
                {
                    cRulesList[x].Status = Status.Approved;
                    break;
                } 
            }
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(ruleIdid))
                {
                    fCRulesList[x].Status = Status.Approved;
                    break;
                }
            }
        }

        public void rejectRule(string ruleId, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
        {
            for (int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(ruleId))
                {
                    cRulesList[x].Status = Status.Rejected;
                    break;
                }
            }
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(ruleId))
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

        

        public int userRelatedApprovedRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = requestApprovedRulesList(cRulesList, fCRulesList);
            return countUserRelatedRule(user, newList);
        }

        public int userRelatedRejectedRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = requestRejectedRulesList(cRulesList, fCRulesList);
            return countUserRelatedRule(user, newList);
        }

        public int userRelatedPendingRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = requestPendingRulesList(cRulesList, fCRulesList);
            return countUserRelatedRule(user, newList);
        }

        public int userSuccessRate(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            int approved = userRelatedApprovedRulesNum(user, cRulesList, fCRulesList);
            int rejected = userRelatedRejectedRulesNum(user, cRulesList, fCRulesList);
            int rate = approved / (approved + rejected);
            return rate;
        }

        public int overallAveSuccessRate(List<User> userList, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            int rateSum = 0;
            foreach(User user in userList)
            {
                rateSum = rateSum + userSuccessRate(user, cRulesList, fCRulesList);
            }
            return rateSum / userList.Count;
        }
    }
}
