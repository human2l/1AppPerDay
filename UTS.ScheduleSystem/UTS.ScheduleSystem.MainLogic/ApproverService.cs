using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class ApproverService
    {
        private DataHandler dataHandler;

        public ApproverService()
        {
            dataHandler = new DataHandler();
        }

        private List<Rule> TraversalList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newRulesList = new List<Rule>();
            foreach (Rule rule in cRulesList)
                newRulesList.Add(rule);
            foreach (Rule rule in fCRulesList)
                newRulesList.Add(rule);
            //{
            //    if (rule.Status.Equals(status))
            //        newRulesList.Add(rule);
            //}
            //foreach (Rule rule in fCRulesList)
            //{
            //    if (rule.Status.Equals(status))
            //        newRulesList.Add(rule);
            //}
            return newRulesList;
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

        private List<Rule> GetRuleListFromDatabaseAccordingToStatus(Status status)
        {
            List<ConversationalRule> conversationalRules = dataHandler.FindConversationalRulesAccordingToStatus(status);
            List<FixedConversationalRule> fixedConversationalRules = dataHandler.FindFixedConversationalRulesAccordingToStatus(status);
            List<Rule> pRulesList = TraversalList(conversationalRules, fixedConversationalRules);
            return pRulesList;
        }

        public List<Rule> RequestPendingRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Pending);
        }

        public List<Rule> RequestRejectedRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Rejected);
        }

        public List<Rule> RequestApprovedRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Approved);
        }

        public void ApproveRule(string ruleId)
        {
            if (ruleId.StartsWith("c"))
            {
                ApproveRuleInConversationalRuleList(ruleId);
                //dataHandler.ChangeOnConversationalRule(string ruleId);
                //ApproveRuleInConversationalRuleList(ruleId, ref cRule);
            }
            else
            {
                ApproveRuleInFixedConversationalRuleList(ruleId);
                //dataHandler.ChangeOnFixedConversationalRule(string ruleId);
                //ApproveRuleInFixedConversationalRuleList(ruleId, ref fCRulesList);
            }
        }

        private void ApproveRuleInConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeConversationalRuleState(ruleId, Status.Approved.ToString());
            //for (int x = 0; x < fCRulesList.Count; x++)
            //{
            //    if (fCRulesList[x].Id.Equals(ruleId))
            //    {
            //        fCRulesList[x].Status = Status.Approved;
            //        break;
            //    }
            //}
        }

        private void ApproveRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Approved.ToString());
            //for (int x = 0; x < cRulesList.Count; x++)
            //{
            //    if (cRulesList[x].Id.Equals(ruleId))
            //    {
            //        cRulesList[x].Status = Status.Approved;
            //        break;
            //    }
            //}
        }

        public void RejectRule(string ruleId)
        {
            if (ruleId.StartsWith("c"))
            {
                RejectRuleInConversationalRuleList(ruleId);
            }
            else
            {
                RejectRuleInFixedConversationalRuleList(ruleId);
            }
        }

        public void RejectRuleInConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeConversationalRuleState(ruleId, Status.Rejected.ToString());
            //for (int x = 0; x < fCRulesList.Count; x++)
            //{
            //    if (fCRulesList[x].Id.Equals(ruleId))
            //    {
            //        fCRulesList[x].Status = Status.Rejected;
            //        break;
            //    }
            //}
        }

        public void RejectRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Rejected.ToString());
            //for (int x = 0; x < cRulesList.Count; x++)
            //{
            //    if (cRulesList[x].Id.Equals(ruleId))
            //    {
            //        cRulesList[x].Status = Status.Rejected;
            //        break;
            //    }
            //}
        }

        public int ApprovedRulesNum()
        {
            List<Rule> approvedRulesList = RequestApprovedRulesList();
            return approvedRulesList.Count;
        }

        public int RejectedRulesNum()
        {
            List<Rule> rejectedRulesList = RequestRejectedRulesList();
            return rejectedRulesList.Count;
        }

        public double SuccessRate()
        {
            double approved = ApprovedRulesNum();
            double rejected = RejectedRulesNum();
            double rate = 0;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        public int UserRelatedApprovedRulesNum(User user)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestApprovedRulesList();
            return CountUserRelatedRule(user, newList);
        }

        public int UserRelatedRejectedRulesNum(User user)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestRejectedRulesList();
            return CountUserRelatedRule(user, newList);
        }

        public int UserRelatedPendingRulesNum(User user)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestPendingRulesList();
            return CountUserRelatedRule(user, newList);
        }

        public double UserSuccessRate(User user)
        {
            double approved = UserRelatedApprovedRulesNum(user);
            double rejected = UserRelatedRejectedRulesNum(user);
            double rate;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        public double OverallAveSuccessRate(List<User> userList, List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            double rateSum = 0;
            foreach(User user in userList)
            {
                rateSum = rateSum + UserSuccessRate(user);
            }
            return rateSum / Convert.ToDouble(userList.Count);
        }
    }
}
