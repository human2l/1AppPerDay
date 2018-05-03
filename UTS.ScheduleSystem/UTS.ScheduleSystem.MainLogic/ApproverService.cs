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

        // Traversal and merge a conversational rule list and a fixed conversational rule list
        private List<Rule> TraversalList(List<ConversationalRule> cRulesList, List<FixedConversationalRule> fCRulesList)
        {
            List<Rule> newRulesList = new List<Rule>();
            foreach (Rule rule in cRulesList)
                newRulesList.Add(rule);
            foreach (Rule rule in fCRulesList)
                newRulesList.Add(rule);
            return newRulesList;
        }

        // Return an editors list from database
        public List<User> RequestEditorList(List<User> users)
        {
            List<User> editorList = new List<User>();
            foreach(User user in users)
            {
                if (user.Role.ToString().Contains("E"))
                    editorList.Add(user);
            }
            return editorList;
        }

        // Return a number of rules related to a user in a rule list
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

        // Get rule list from database specified on status
        private List<Rule> GetRuleListFromDatabaseAccordingToStatus(Status status)
        {
            List<ConversationalRule> conversationalRules = dataHandler.FindConversationalRulesAccordingToStatus(status);
            List<FixedConversationalRule> fixedConversationalRules = dataHandler.FindFixedConversationalRulesAccordingToStatus(status);
            List<Rule> pRulesList = TraversalList(conversationalRules, fixedConversationalRules);
            return pRulesList;
        }

        // Return pending rule list
        public List<Rule> RequestPendingRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Pending);
        }

        // Return rejected rule list
        public List<Rule> RequestRejectedRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Rejected);
        }

        // Return approved rule list
        public List<Rule> RequestApprovedRulesList()
        {
            return GetRuleListFromDatabaseAccordingToStatus(Status.Approved);
        }

        // Approve a rule in database
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

        //Subfunction of ApproveRule to approve a conversational rule 
        private void ApproveRuleInConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeConversationalRuleState(ruleId, Status.Approved.ToString());
        }

        //Subfunction of ApproveRule to approve a fixed conversational rule 
        private void ApproveRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Approved.ToString());
        }

        // Reject a rule in database
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

        //Subfunction of RejectRule to reject a conversational rule 
        private void RejectRuleInConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeConversationalRuleState(ruleId, Status.Rejected.ToString());
        }

        //Subfunction of RejectRule to reject a fixed conversational rule 
        private void RejectRuleInFixedConversationalRuleList(string ruleId)
        {
            dataHandler.ChangeFixedConversationalRuleState(ruleId, Status.Rejected.ToString());
        }

        // Return a count number of approved rule in database
        public int ApprovedRulesNum()
        {
            int conversationalRuleNum = dataHandler.FindConversationalRuleNum(Status.Approved.ToString());
            int fixedConversationalRuleNum = dataHandler.FindFixedConversationalRuleNum(Status.Approved.ToString());
            return conversationalRuleNum + fixedConversationalRuleNum;
        }

        // Return a count number of rejected rule in database
        public int RejectedRulesNum()
        {
            int conversationalRuleNum = dataHandler.FindConversationalRuleNum(Status.Rejected.ToString());
            int fixedConversationalRuleNum = dataHandler.FindFixedConversationalRuleNum(Status.Rejected.ToString());
            return conversationalRuleNum + fixedConversationalRuleNum;
        }

        // Return success rule rate
        public double SuccessRate()
        {
            double approved = ApprovedRulesNum();
            double rejected = RejectedRulesNum();
            double rate = 0;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        // Return a count number of user related approved rule
        public int UserRelatedApprovedRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestApprovedRulesList();
            return CountUserRelatedRule(id, newList);
        }

        // Return a count number of user related rejected rule
        public int UserRelatedRejectedRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestRejectedRulesList();
            return CountUserRelatedRule(id, newList);
        }

        // Return a count number of user related pending rule
        public int UserRelatedPendingRulesNum(string id)
        {
            List<Rule> newList = new List<Rule>();
            newList = RequestPendingRulesList();
            return CountUserRelatedRule(id, newList);
        }

        // Return a success rate of user related rules
        public double UserSuccessRate(string id)
        {
            double approved = UserRelatedApprovedRulesNum(id);
            double rejected = UserRelatedRejectedRulesNum(id);
            double rate;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        // Return an overall average success rate
        public double OverallAveSuccessRate()
        {
            List<string> editorIds = dataHandler.FindEditorsId();
            double rateSum = 0;
            foreach(string id in editorIds)
            {
                rateSum = rateSum + UserSuccessRate(id);
            }
            return rateSum / Convert.ToDouble(dataHandler.FindEditorNum());
        }
        
        // Recognize the on selected row editor and save into on focus user
        public User RecognizeUser(string editorId, List<User> editorList)
        {
            User result = new User();
            foreach (User user in editorList)
            {
                if (user.Id.Equals(editorId))
                {
                    result = user;
                    break;
                }
            }
            return result;
        }
    }
}
