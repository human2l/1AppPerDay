using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic
{
    public class ApproverService
    {
        public ApproverService()
        {

        }

        // Return an editors list from database
        public List<AspNetUser> RequestEditorList()
        {
            List<AspNetUser> editorList = new List<AspNetUser>();
            List<AspNetUser> users = UserHandler.UsersList();
            foreach (AspNetUser user in users)
            {
                if (user.Role != null && user.Role.ToString().Contains("E"))
                    editorList.Add(user);
            }
            return editorList;
        }

        // Return a number of rules related to a user in a rule list
        private int CountUserRelatedConversationalRule(string id, List<ConversationalRule> rules)
        {
            int result = 0;
            foreach (var rule in rules)
            {
                if (rule.RelatedUsersId.Contains(id))
                    result++;
            }
            return result;
        }

        // Return a number of rules related to a user in a rule list
        private int CountUserRelatedFixConversationalRule(string id, List<FixedConversationalRule> rules)
        {
            int result = 0;
            foreach (var rule in rules)
            {
                if (rule.RelatedUsersId.Contains(id))
                    result++;
            }
            return result;
        }

        // Get conversational rule list from database specified on status
        private List<ConversationalRule> GetConversationalRuleListFromDatabaseAccordingToStatus(Status status)
        {
            List<ConversationalRule> statusFiltedConversationalRules = new List<ConversationalRule>();
            List<ConversationalRule> conversationalRules = ConversationalRuleHandler.FindAllConversationalRules();
            foreach (ConversationalRule rule in conversationalRules)
            {
                if (rule.Status == status.ToString())
                    statusFiltedConversationalRules.Add(rule);
            }
            return statusFiltedConversationalRules;
        }

        // Get fixed conversational rule list from database specified on status
        private List<FixedConversationalRule> GetFixedConversationalRuleListFromDatabaseAccordingToStatus(Status status)
        {
            List<FixedConversationalRule> statusFiltedFixedConversationalRules = new List<FixedConversationalRule>();
            List<FixedConversationalRule> fixedConversationalRules = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            foreach (FixedConversationalRule rule in fixedConversationalRules)
            {
                if (rule.Status == status.ToString())
                    statusFiltedFixedConversationalRules.Add(rule);
            }
            return statusFiltedFixedConversationalRules;
        }

        // Return pending conversational rule list
        public List<ConversationalRule> RequestPendingConversationalRulesList()
        {
            return GetConversationalRuleListFromDatabaseAccordingToStatus(Status.Pending);
        }

        // Return pending fixed conversational rule list
        public List<FixedConversationalRule> RequestPendingFixedConversationalRulesList()
        {
            return GetFixedConversationalRuleListFromDatabaseAccordingToStatus(Status.Pending);
        }

        // Return rejected conversational rule list
        public List<ConversationalRule> RequestRejectedConversationalRulesList()
        {
            return GetConversationalRuleListFromDatabaseAccordingToStatus(Status.Rejected);
        }

        // Return rejected fixed conversational rule list
        public List<FixedConversationalRule> RequestRejectedFixedConversationalRulesList()
        {
            return GetFixedConversationalRuleListFromDatabaseAccordingToStatus(Status.Rejected);
        }

        // Return pending conversational rule list
        public List<ConversationalRule> RequestApprovedConversationalRulesList()
        {
            return GetConversationalRuleListFromDatabaseAccordingToStatus(Status.Approved);
        }

        // Return pending fixed conversational rule list
        public List<FixedConversationalRule> RequestApprovedFixedConversationalRulesList()
        {
            return GetFixedConversationalRuleListFromDatabaseAccordingToStatus(Status.Approved);
        }

        //Subfunction of ApproveRule to approve a conversational rule 
        public void ApproveConversationalRule(string ruleId)
        {
            ConversationalRule conversationalRule = ConversationalRuleHandler.FindConversationalRuleById(ruleId);
            conversationalRule.Status = Status.Approved.ToString();
            ConversationalRuleHandler.UpdateAConversationalRule(conversationalRule);
        }

        //Subfunction of ApproveRule to approve a fixed conversational rule 
        public void ApproveFixedConversationalRule(string ruleId)
        {
            FixedConversationalRule fixedConversationalRule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(ruleId);
            fixedConversationalRule.Status = Status.Approved.ToString();
            FixedConversationalRuleHandler.UpdateAFixedConversationalRule(fixedConversationalRule);
        }

        //Subfunction of RejectRule to reject a conversational rule 
        public void RejectRuleInConversationalRuleList(string ruleId)
        {
            ConversationalRule conversationalRule = ConversationalRuleHandler.FindConversationalRuleById(ruleId);
            conversationalRule.Status = Status.Rejected.ToString();
            ConversationalRuleHandler.UpdateAConversationalRule(conversationalRule);
        }

        //Subfunction of RejectRule to reject a fixed conversational rule 
        public void RejectRuleInFixedConversationalRuleList(string ruleId)
        {
            FixedConversationalRule fixedConversationalRule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(ruleId);
            fixedConversationalRule.Status = Status.Rejected.ToString();
            FixedConversationalRuleHandler.UpdateAFixedConversationalRule(fixedConversationalRule);
        }

        // Return a count number of approved rule in database
        public int ApprovedRulesNum()
        {
            return RequestApprovedConversationalRulesList().Count + RequestApprovedFixedConversationalRulesList().Count;
        }

        // Return a count number of rejected rule in database
        public int RejectedRulesNum()
        {
            return RequestRejectedConversationalRulesList().Count + RequestRejectedFixedConversationalRulesList().Count;
        }

        // Return success rule rate
        public double SuccessRate()
        {
            double approved = ApprovedRulesNum();
            double rejected = RejectedRulesNum();
            double rate = 0;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate * 100;
        }

        // Return a count number of user related approved rule
        public int UserRelatedApprovedRulesNum(string userId)
        {
            return CountUserRelatedConversationalRule(userId, RequestApprovedConversationalRulesList()) + 
                CountUserRelatedFixConversationalRule(userId, RequestApprovedFixedConversationalRulesList());
        }

        // Return a count number of user related rejected rule
        public int UserRelatedRejectedRulesNum(string userId)
        {
            return CountUserRelatedConversationalRule(userId, RequestRejectedConversationalRulesList()) +
                CountUserRelatedFixConversationalRule(userId, RequestRejectedFixedConversationalRulesList());
        }

        // Return a count number of user related pending rule
        public int UserRelatedPendingRulesNum(string userId)
        {
            return CountUserRelatedConversationalRule(userId, RequestPendingConversationalRulesList()) +
                CountUserRelatedFixConversationalRule(userId, RequestPendingFixedConversationalRulesList());
        }

        // Return a success rate of user related rules
        public double UserSuccessRate(string userId)
        {
            double approved = UserRelatedApprovedRulesNum(userId);
            double rejected = UserRelatedRejectedRulesNum(userId);
            double rate;
            rate = (approved + rejected) == 0 ? 0 : approved / (approved + rejected);
            return rate;
        }

        // Return an overall average success rate
        public double OverallAveSuccessRate()
        {
            List<AspNetUser> editors = RequestEditorList();
            double rateSum = 0;
            foreach(AspNetUser editor in editors)
            {
                rateSum = rateSum + UserSuccessRate(editor.Email);
            }
            return rateSum / Convert.ToDouble(editors.Count);
        }
    }
}
