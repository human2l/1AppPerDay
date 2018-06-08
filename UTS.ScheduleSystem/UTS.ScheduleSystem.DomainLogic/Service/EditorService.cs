using System;
using System.Collections.Generic;
using System.Linq;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic
{
    public class EditorService
    {
        public EditorService()
        {
        }

        // Add a fixed conversation rule
        public void AddNewFCRule(string input, string output, string userId)
        {
            FixedConversationalRule rule = new FixedConversationalRule
            {
                Input = Utils.IgnoreSpace(input),
                Output = Utils.IgnoreSpace(output),
                RelatedUsersId = userId,
                Status = Status.Pending.ToString()
            };
            FixedConversationalRuleHandler.AddFixedConversationalRule(rule);
        }

        // Add a conversation rule
        public void AddNewCRule(string input, string output, string userId)
        {
            ConversationalRule rule = new ConversationalRule
            {
                Input = Utils.IgnoreSpace(input),
                Output = Utils.IgnoreSpace(output),
                RelatedUsersId = userId,
                Status = Status.Pending.ToString()
            };
            ConversationalRuleHandler.AddConversationalRule(rule);
        }

        public List<FixedConversationalRule> ShowAllFixedConversationalRuleRules()
        {
            return FixedConversationalRuleHandler.FindAllFixedConversationalRules();
        }

        public List<ConversationalRule> ShowAllConversationalRuleRules()
        {
            return ConversationalRuleHandler.FindAllConversationalRules();
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

        

        // Show a user's approved rules
        public Tuple<List<FixedConversationalRule>, List<ConversationalRule>> ShowCurrentUserApprovedRules(string userName)
        {
            List<FixedConversationalRule> userRelatedfCRules = new List<FixedConversationalRule>();
            List<FixedConversationalRule> fCRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            List<ConversationalRule> userRelatedcRules = new List<ConversationalRule>();
            List<ConversationalRule> cRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            foreach (FixedConversationalRule rule in fCRulesList)
            {
                if(rule.Status == Status.Approved.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedfCRules.Add(rule);
                        }
                    }
                }
            }
            foreach (ConversationalRule rule in cRulesList)
            {
                if (rule.Status == Status.Approved.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedcRules.Add(rule);
                        }
                    }
                }
            }
            return Tuple.Create(userRelatedfCRules, userRelatedcRules);
        }

        // Show a user's rejected rules
        public List<Rule> ShowCurrentUserRejectedRules(string userName)
        {
            List<Rule> userRelatedRules = new List<Rule>();
            List<FixedConversationalRule> fCRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            List<ConversationalRule> cRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            foreach (FixedConversationalRule rule in fCRulesList)
            {
                if (rule.Status == Status.Rejected.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            foreach (ConversationalRule rule in cRulesList)
            {
                if (rule.Status == Status.Rejected.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedRules.Add(rule);
                        }
                    }
                }
            }
            return userRelatedRules;
        }

        // Show a user's pengding rules
        public Tuple<List<FixedConversationalRule>, List<ConversationalRule>> ShowCurrentUserPendingRules(string userName)
        {
            List<FixedConversationalRule> userRelatedfCRules = new List<FixedConversationalRule>();
            List<FixedConversationalRule> fCRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            List<ConversationalRule> userRelatedcRules = ConversationalRuleHandler.FindAllConversationalRules();
            List<ConversationalRule> cRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            foreach (FixedConversationalRule rule in fCRulesList)
            {
                if (rule.Status == Status.Pending.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedfCRules.Add(rule);
                        }
                    }
                }
            }
            foreach (ConversationalRule rule in cRulesList)
            {
                if (rule.Status == Status.Pending.ToString())
                {
                    string[] relatedUserId = rule.RelatedUsersId.Split(' ');
                    for (int i = 0; i < relatedUserId.Length; i++)
                    {
                        if (relatedUserId[i] == userName)
                        {
                            userRelatedcRules.Add(rule);
                        }
                    }
                }
            }
            return Tuple.Create(userRelatedfCRules, userRelatedcRules);
        }

        // Show the count of a user's approved rules
        public int ShowCurrentUserApprovedRulesCount(string userName)
        {
            int count = ShowCurrentUserApprovedRules(userName).Item1.Count + ShowCurrentUserApprovedRules(userName).Item2.Count;
            return count;
        }

        // Show the count of a user's rejected rules
        public int ShowCurrentUserRejectedRulesCount(string userName)
        {
            int count = ShowCurrentUserRejectedRules(userName).Count;
            return count;
        }

        // Show the count of a user's pending rules
        public int ShowCurrentUserPendingRulesCount(string userName)
        {
            int count = ShowCurrentUserPendingRules(userName).Item1.Count + ShowCurrentUserPendingRules(userName).Item2.Count;
            return count;
        }

        // Show the percentage of a user's approved rules
        public double ShowCurrentUserSuccessRate(string userName)
        {
            double approvedCount = ShowCurrentUserApprovedRulesCount(userName);
            double rejectedCount = ShowCurrentUserRejectedRulesCount(userName);
            double pendingCount = ShowCurrentUserPendingRulesCount(userName);
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
            string az = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string num = "1234567890";
            if(input == null || output == null || input.Length == 0 || output.Length == 0)
            {
                return false;
            }

            foreach(char x in input)
            {
                if(az.Contains(x)||num.Contains(x)||x == ' ')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            foreach (char x in output)
            {
                if (az.Contains(x) || num.Contains(x) || x == ' ')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // Make sure the input only contains one coloum
        public bool IsRuleValid(string input)
        {
            string az = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string num = "1234567890";
            string marks = "{}";
            string[] keywordsArray = { "topic", "participants", "location", "startdate", "enddate" };


            if (input == null || input.Length == 0)
            {
                return false;
            }
            foreach (char x in input)
            {
                if (az.Contains(x) || num.Contains(x) || marks.Contains(x) || x == ' ')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            int leftBrackets = 0;
            int rightBrackets = 0;
            int keywordIndex = -1;
            int keywordEndIndex = -1;

            

            for (int i = 0; i < input.Length-2; i++)
            {
                if(input[i] == '{' && input[i+1] == ' ')
                {
                    leftBrackets++;
                    keywordIndex = i + 2;
                }
            }

            for(int i = 1; i < input.Length-1; i++)
            {
                if (input[i] == ' ' && input[i + 1] == '}')
                {
                    rightBrackets++;
                    keywordEndIndex = i - 1;
                }
            }

            if(leftBrackets != 1 || rightBrackets != 1 || keywordIndex == -1 || keywordEndIndex == -1 || keywordIndex >= keywordEndIndex)
            {
                return false;
            }

            return (keywordsArray.Contains(input.Substring(keywordIndex, keywordEndIndex - keywordIndex+1))) ;
         
        }
        
       
        // Check whether a rule is already existed in the database or not
        public bool CheckRepeatingRule(string input)
        {
            List<FixedConversationalRule> fCRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            List< ConversationalRule > cRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            string compactedString = Utils.IgnoreSpace(input);

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