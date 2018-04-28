using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class ApproverService
    {
       
        public ApproverService()
        {

        }

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
            return rule.LastRelatedUserID;
        }

        public List<User> RequestEditorList(List<User> userList)
        {
            List<User> editorList = new List<User>();
            foreach(User user in userList)
            {
                if (user.Role.ToString().Contains("E"))
                    editorList.Add(user);
            }
            return editorList;
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

        public void ApproveRule(string ruleId, ref List<ConversationalRule> cRule, ref List<FixedConversationalRule> fCRulesList)
        {
            if (ruleId.StartsWith("c"))
            {
                ApproveRuleInConversationalRuleList(ruleId, ref cRule);
            }
            else
            {
                ApproveRuleInFixedConversationalRuleList(ruleId, ref fCRulesList);
            }
        }

        private void ApproveRuleInFixedConversationalRuleList(string ruleId, ref List<FixedConversationalRule> fCRulesList)
        {
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(ruleId))
                {
                    fCRulesList[x].Status = Status.Approved;
                    break;
                }
            }
        }

        private void ApproveRuleInConversationalRuleList(string ruleId, ref List<ConversationalRule> cRulesList)
        {
            for (int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(ruleId))
                {
                    cRulesList[x].Status = Status.Approved;
                    break;
                }
            }
        }

        public void RejectRule(string ruleId, ref List<ConversationalRule> cRule, ref List<FixedConversationalRule> fCRulesList)
        {
            if (ruleId.StartsWith("c"))
            {
                RejectRuleInConversationalRuleList(ruleId, ref cRule);
            }
            else
            {
                RejectRuleInFixedConversationalRuleList(ruleId, ref fCRulesList);
            }
        }

        public void RejectRuleInFixedConversationalRuleList(string ruleId, ref List<FixedConversationalRule> fCRulesList)
        {
            for (int x = 0; x < fCRulesList.Count; x++)
            {
                if (fCRulesList[x].Id.Equals(ruleId))
                {
                    fCRulesList[x].Status = Status.Rejected;
                    break;
                }
            }
        }

        public void RejectRuleInConversationalRuleList(string ruleId, ref List<ConversationalRule> cRulesList)
        {
            for (int x = 0; x < cRulesList.Count; x++)
            {
                if (cRulesList[x].Id.Equals(ruleId))
                {
                    cRulesList[x].Status = Status.Rejected;
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
