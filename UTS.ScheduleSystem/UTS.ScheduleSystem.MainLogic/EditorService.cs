using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class EditorService
    {
        private DataHandler dataHandler;

        public EditorService()
        {
            dataHandler = new DataHandler();
        }

        // Add a fixed conversation rule
        public List<FixedConversationalRule> AddNewFCRule(string input, string output, string userId, List<FixedConversationalRule> fCRulesList)
        {
            FixedConversationalRule rule = new FixedConversationalRule(Utils.CreateIdByType("FixedConversationalRule", dataHandler.FindLastFixedConversationalRuleId()), input, output, userId, Status.Pending);
            fCRulesList.Add(rule);
            return fCRulesList;
        }

        // Add a conversation rule
        public List<ConversationalRule> AddNewCRule(string input, string output, string userId, List<ConversationalRule> cRulesList)
        {
            ConversationalRule rule = new ConversationalRule(Utils.CreateIdByType("ConversationalRule", dataHandler.FindLastConversationalRuleId()), input, output, userId, Status.Pending);
            cRulesList.Add(rule);
            return cRulesList;
        }

        // Show all pending rules stored in the database
        public List<Rule> ShowAllPendingRules(List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            List<Rule> pendingRulesList = new List<Rule>();
            foreach (FixedConversationalRule fCRule in fCRulesList)
            {
                if (fCRule.Status == Status.Pending)
                {
                    pendingRulesList.Add(fCRule);
                }
            }
            foreach (ConversationalRule cRule in cRulesList)
            {
                if (cRule.Status == Status.Pending)
                {
                    pendingRulesList.Add(cRule);
                }
            }
            return pendingRulesList;
        }

        // Show all rejected rules stored in the database
        public List<Rule> ShowAllRejectedRules(List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            List<Rule> rejectedRulesList = new List<Rule>();
            foreach (FixedConversationalRule fCRule in fCRulesList)
            {
                if (fCRule.Status == Status.Rejected)
                {
                    rejectedRulesList.Add(fCRule);
                }
            }
            foreach (ConversationalRule cRule in cRulesList)
            {
                if (cRule.Status == Status.Rejected)
                {
                    rejectedRulesList.Add(cRule);
                }
            }
            return rejectedRulesList;
        }

        // Change a rule
        public Tuple<List<FixedConversationalRule>, List<ConversationalRule>> EditPendingRule(string userId, string ruleId, string ruleInput, string ruleOutput, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            bool valueChanged = false;
            for(int i=0; i<fCRulesList.Count; i++)
            {
                if(fCRulesList[i].Id == ruleId)
                {
                    fCRulesList[i].Input = ruleInput;
                    fCRulesList[i].Output = ruleOutput;
                    fCRulesList[i].LastRelatedUserID = userId;
                    fCRulesList[i].RelatedUsersId += " " + userId;
                    valueChanged = true;
                    break;
                }
            }
            if (!valueChanged)
            {
                for (int i = 0; i < cRulesList.Count; i++)
                {
                    if (cRulesList[i].Id == ruleId)
                    {
                        cRulesList[i].Input = ruleInput;
                        cRulesList[i].Output = ruleOutput;
                        cRulesList[i].LastRelatedUserID = userId;
                        cRulesList[i].RelatedUsersId += " " + userId;
                        break;
                    }
                }
            }
            return Tuple.Create(fCRulesList, cRulesList);
        }

        // Delete a rule
        public Tuple<List<FixedConversationalRule>, List<ConversationalRule>> DeletePendingRule(string ruleId, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            FixedConversationalRule fCRule = FindFCRule(ruleId, fCRulesList);
            if (fCRule != null)
            {
                fCRulesList.Remove(fCRule);
            }
            else
            {
                ConversationalRule cRule = FindCRule(ruleId, cRulesList);
                if (cRule != null)
                {
                    cRulesList.Remove(cRule);
                }
            }
            return Tuple.Create(fCRulesList, cRulesList);
        }

        // Show a user's approved rules
        public List<Rule> ShowCurrentUserApprovedRules(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            List<Rule> userRelatedRules = new List<Rule>();
            foreach(Rule rule in fCRulesList)
            {
                if(rule.Status == Status.Approved)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status == Status.Approved)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            return userRelatedRules;
        }

        // Show a user's rejected rules
        public List<Rule> ShowCurrentUserRejectedRules(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            List<Rule> userRelatedRules = new List<Rule>();
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status == Status.Rejected)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status == Status.Rejected)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            return userRelatedRules;
        }

        // Show a user's pengding rules
        public List<Rule> ShowCurrentUserPendingRules(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            List<Rule> userRelatedRules = new List<Rule>();
            foreach (Rule rule in fCRulesList)
            {
                if (rule.Status == Status.Pending)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            foreach (Rule rule in cRulesList)
            {
                if (rule.Status == Status.Pending)
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == user.Id)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            return userRelatedRules;
        }

        // Show the count of a user's approved rules
        public int ShowCurrentUserApprovedRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserApprovedRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        // Show the count of a user's rejected rules
        public int ShowCurrentUserRejectedRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserRejectedRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        // Show the count of a user's pending rules
        public int ShowCurrentUserPendingRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserPendingRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        // Show the percentage of a user's approved rules
        public double ShowCurrentUserSuccessRate(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            double approvedCount = ShowCurrentUserApprovedRulesCount(user, fCRulesList, cRulesList);
            double rejectedCount = ShowCurrentUserRejectedRulesCount(user, fCRulesList, cRulesList);
            double pendingCount = ShowCurrentUserPendingRulesCount(user, fCRulesList, cRulesList);
            if (approvedCount + rejectedCount + pendingCount != 0)
            {
                double rate = Math.Round (approvedCount / (approvedCount + rejectedCount + pendingCount), 2);
                return rate;
            }
            return 0;
        }

        // Check the input validation
        public bool IsFixedRuleValid (string input, string output)
        {
            if (!input.Contains("{") && !input.Contains("}") && !output.Contains("{") && !output.Contains("}"))
            {
                return true;
            }
            return false;
        }

        // Make sure the input only contains one coloum
        public bool IsRuleValid(string input)
        {
            //string compactedString = IgnoreWhiteSpace(input);

            string[] phrase1;
            string[] phrase2;
            phrase1 = input.Split('{');
            phrase2 = input.Split('}');
            string[] words1;
            string[] words2;
            if (phrase1.Count() > 1 && phrase2.Count() > 1)
            {
                words1 = phrase1[1].Split(' ');
                words2 = phrase2[0].Split(' ');
                if (words1.Count() > 1 && words2.Count() > 1 && words1[1] == words2[words2.Count() - 2] && phrase1.Count() == 2 && phrase2.Count() == 2 && (words1[1] == "topic" ||
                        words1[1] == "participants" || words1[1] == "location" || words1[1] == "startdate" || words1[1] == "enddate"))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        
        private FixedConversationalRule FindFCRule(string ruleId, List<FixedConversationalRule> rulesList)
        {
            foreach (FixedConversationalRule rule in rulesList)
            {
                if (rule.Id == ruleId)
                {
                    return rule;
                }
            }
            return null;
        }

        private ConversationalRule FindCRule(string ruleId, List<ConversationalRule> rulesList)
        {
            foreach (ConversationalRule rule in rulesList)
            {
                if (rule.Id == ruleId)
                {
                    return rule;
                }
            }
            return null;
        }

        // Check whether a rule is already existed in the database or not
        public bool CheckRepeatingRule(string input, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            string compactedString = IgnoreWhiteSpace(input);

            foreach (FixedConversationalRule r in fCRulesList)
            {
                if (compactedString == r.Input)
                {
                    return true;
                }
            }
            foreach (ConversationalRule r in cRulesList)
            {
                if (compactedString == r.Input)
                {
                    return true;
                }
            }
            return false;
        }

        protected string IgnoreWhiteSpace (string input)
        {
            char[] WhiteSpace = new char[] { ' ' };
            string longString = input;
            string[] split = longString.Split(WhiteSpace, StringSplitOptions.RemoveEmptyEntries);
            string compactedString = string.Join(" ", split);
            return compactedString;
        }
    }
}