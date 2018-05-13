using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

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
        public void AddNewFCRule(string input, string output, string userId)
        {
            FixedConversationalRule rule = new FixedConversationalRule(Utils.CreateIdByType("FixedConversationalRule", dataHandler.FindLastFixedConversationalRuleId()), Utils.IgnoreWhiteSpace(input), Utils.IgnoreWhiteSpace(output), userId, Status.Pending);
            dataHandler.AddFixedConversationalRule(rule);
        }

        // Add a conversation rule
        public void AddNewCRule(string input, string output, string userId)
        {
            //ConversationalRule rule = new ConversationalRule(Utils.CreateIdByType("ConversationalRule", dataHandler.FindLastConversationalRuleId()), Utils.IgnoreWhiteSpace(input), Utils.IgnoreWhiteSpace(output), userId, Status.Pending);
            //dataHandler.AddConversationalRule(rule);
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

        // Get related user id string and return a string without target user id
        private string StringFormat(string relatedUserId, string userId)
        {
            string result = "";
            if (relatedUserId.Contains(userId))
            {
                string[] relatedUserIds = relatedUserId.Split(' ');
                foreach(string id in relatedUserIds)
                {
                    if (!id.Equals(userId))
                        result = result + id;
                }
            }
            else
                result = relatedUserId;
            return result;
        }

        // Change a rule
        public void EditPendingRule(string userId, string ruleId, string ruleInput, string ruleOutput, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            string relatedUserId = userId;

            if (ruleId.Contains("fc"))
            {
                for (int i = 0; i < fCRulesList.Count; i++)
                {
                    if (fCRulesList[i].Id == ruleId)
                    {
                        relatedUserId = (StringFormat(fCRulesList[i].RelatedUsersId,userId) + " " + userId).Trim();
                        break;
                    }
                }
                dataHandler.UpdateAFixedConversationalRule(ruleInput, ruleOutput, relatedUserId, "Pending", ruleId);
            }
            else if (ruleId.Contains("c"))
            {
                for (int i = 0; i < cRulesList.Count; i++)
                {
                    if (cRulesList[i].Id == ruleId)
                    {
                        relatedUserId = (StringFormat(cRulesList[i].RelatedUsersId, userId) + " " + userId).Trim();
                        break;
                    }
                }
                dataHandler.UpdateAConversationalRule(ruleInput, ruleOutput, relatedUserId, "Pending", ruleId);
            }
        }

        // Delete a rule
        public void DeletePendingRule(string ruleId)
        {
            if (ruleId.Contains("fc"))
            {
                dataHandler.RemoveFixedConversationalRule(ruleId);
            }
            else
            {
                if (ruleId.Contains("c"))
                {
                    dataHandler.RemoveConversationalRule(ruleId);
                }
            }
        }

        // Show a user's approved rules
        public List<Rule> ShowCurrentUserApprovedRules(AspNetUser user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
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
            string az = "qwertyuiopasdfghjklzxcvbnm ";
            string num = "1234567890";
            if (!input.Contains("{") && !input.Contains("}") && !output.Contains("{") && !output.Contains("}"))
            {
                input = input.ToLower();
                foreach(char x in input)
                {
                    if(!az.Contains(x) && !num.Contains(x))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        // Make sure the input only contains one coloum
        public bool IsRuleValid(string input)
        {
            //string compactedString = IgnoreWhiteSpace(input);
            string az = "qwertyuiopasdfghjklzxcvbnm ";
            string num = "1234567890";
            string marks = "{}";
            input = input.ToLower();
            foreach (char x in input)
            {
                if (!az.Contains(x) && !num.Contains(x) && !marks.Contains(x))
                {
                    return false;
                }
            }
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
        
       public Rule FindRuleById(string id)
        {
            Rule rule = null;
            if (id.Contains("f"))
            {
                rule = dataHandler.FindFixedConversationalRuleById(id);
            }
            else
            {
                rule = dataHandler.FindConversationalRuleById(id);
            }
            

            return rule;
        } 

        // Check whether a rule is already existed in the database or not
        public bool CheckRepeatingRule(string input, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            string compactedString = Utils.IgnoreWhiteSpace(input);

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
    }
}