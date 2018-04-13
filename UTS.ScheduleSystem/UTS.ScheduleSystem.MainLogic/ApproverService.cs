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
        //private Rule findRule(List<ConversationalRule> conversationalRulesList, List<FixedConversationalRule> fixedConversationalRulesList, string id)
        //{
        //    Rule rule = null;
        //    foreach (Rule ruleX in conversationalRulesList)
        //    {
        //        if (ruleX.Id.Equals(id))
        //            rule = ruleX;
        //    }
        //    foreach (Rule ruleX in fixedConversationalRulesList)
        //    {
        //        if (ruleX.Id.Equals(id))
        //            rule = ruleX;
        //    }
        //    return rule;
        //}

        private List<Rule> TraversalList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList, Status status)
        {
            List<Rule> newRulesList = new List<Rule>();
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status.Equals(status))
                    newRulesList.Add(rule);
            }
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status.Equals(status))
                    newRulesList.Add(rule);
            }
            return newRulesList;
        }

        public List<Rule> RequestRejectedRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rjRulesList = TraversalList(cRulesList, fCRulesList, Status.Rejected);
            return rjRulesList;
        }

        private string GetLastUser(Rule rule)
        {
            string relatedUsersId = rule.RelatedUsersId;
            string[] relatedUsersIdString = relatedUsersId.Split(' ');
            string lastUserId = relatedUsersIdString[relatedUsersIdString.Length];
            return lastUserId;
        }
        private int CountUserRelatedRule(User user, List<Rule> rules)
        {
            int result = 0;
            foreach (Rule rule in rules)
            {
                if (rule.RelatedUsersId.Contains(user.Id))
                    result++;
            }
            return result;
        }

        public List<Rule> RequestPendingRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> pRulesList = TraversalList(cRulesList, fCRulesList, Status.Pending);
            return pRulesList;
        }

        public List<Rule> RequestApprovedRulesList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> apvRulesList = TraversalList(cRulesList, fCRulesList, Status.Approved);
            return apvRulesList;
        }


        public void ApproveRule(string ruleIdid, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
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

        public void RejectRule(string ruleId, ref List<ConversationalRule> cRulesList, ref List<FixedConversationalRule> fCRulesList)
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

        
        public int ApprovedRulesNum(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> approvedRulesList = RequestApprovedRulesList(cRulesList, fCRulesList);
            return approvedRulesList.Count;
        }

        public int RejectedRulesNum(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> rejectedRulesList = RequestRejectedRulesList(cRulesList, fCRulesList);
            return rejectedRulesList.Count;
        }

        public double SuccessRate(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            double approved = ApprovedRulesNum(cRulesList, fCRulesList);
            double rejected = RejectedRulesNum(cRulesList, fCRulesList);
            double rate = 0;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        

        public int UserRelatedApprovedRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestApprovedRulesList(cRulesList, fCRulesList);
            return CountUserRelatedRule(user, newList);
        }

        public int UserRelatedRejectedRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestRejectedRulesList(cRulesList, fCRulesList);
            return CountUserRelatedRule(user, newList);
        }

        public int UserRelatedPendingRulesNum(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestPendingRulesList(cRulesList, fCRulesList);
            return CountUserRelatedRule(user, newList);
        }

        public double UserSuccessRate(User user, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            double approved = UserRelatedApprovedRulesNum(user, cRulesList, fCRulesList);
            double rejected = UserRelatedRejectedRulesNum(user, cRulesList, fCRulesList);
            double rate;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        public double OverallAveSuccessRate(List<User> userList, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            double rateSum = 0;
            foreach(User user in userList)
            {
                rateSum = rateSum + UserSuccessRate(user, cRulesList, fCRulesList);
            }
            return rateSum / Convert.ToDouble(userList.Count);
        }
    }
}
