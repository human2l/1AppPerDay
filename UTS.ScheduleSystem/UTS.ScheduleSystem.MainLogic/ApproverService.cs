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
            return newRulesList;
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

        private int CountUserRelatedRule(string id, List<Rule> rules)
        {
            int result = 0;
            foreach (Rule rule in rules)
            {
                if (rule.RelatedUsersId.Contains(id))
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
            }
            else
            {
                ApproveRuleInFixedConversationalRuleList(ruleId);
            }
        }

        private void ApproveRuleInConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeConversationalRuleState(ruleId, Status.Approved.ToString());
        }

        private void ApproveRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Approved.ToString());
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
        }

        public void RejectRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Rejected.ToString());
        }

        public int ApprovedRulesNum()
        {
            int conversationalRuleNum = dataHandler.FindConversationalRuleNum(Status.Approved.ToString());
            int fixedConversationalRuleNum = dataHandler.FindFixedConversationalRuleNum(Status.Approved.ToString());
            return conversationalRuleNum + fixedConversationalRuleNum;
        }

        public int RejectedRulesNum()
        {
            int conversationalRuleNum = dataHandler.FindConversationalRuleNum(Status.Rejected.ToString());
            int fixedConversationalRuleNum = dataHandler.FindFixedConversationalRuleNum(Status.Rejected.ToString());
            return conversationalRuleNum + fixedConversationalRuleNum;
        }

        public double SuccessRate()
        {
            double approved = ApprovedRulesNum();
            double rejected = RejectedRulesNum();
            double rate = 0;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        public int UserRelatedApprovedRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestApprovedRulesList();
            return CountUserRelatedRule(id, newList);
        }

        public int UserRelatedRejectedRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestRejectedRulesList();
            return CountUserRelatedRule(id, newList);
        }

        public int UserRelatedPendingRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestPendingRulesList();
            return CountUserRelatedRule(id, newList);
        }

        public double UserSuccessRate(string id)
        {
            double approved = UserRelatedApprovedRulesNum(id);
            double rejected = UserRelatedRejectedRulesNum(id);
            double rate;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        public double OverallAveSuccessRate()
        {
            List<string> editorIds = dataHandler.FindEditors();
            double rateSum = 0;
            foreach(string id in editorIds)
            {
                rateSum = rateSum + UserSuccessRate(id);
            }
            return rateSum / Convert.ToDouble(dataHandler.FindEditorNum());
        }
    }
}
