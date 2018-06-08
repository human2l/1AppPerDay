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
            //dataHandler.AddFixedConversationalRule(rule);
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
            //dataHandler.AddConversationalRule(rule);
        }

        public List<FixedConversationalRule> ShowAllFixedConversationalRuleRules()
        {
            return FixedConversationalRuleHandler.FindAllFixedConversationalRules();
        }

        public List<ConversationalRule> ShowAllConversationalRuleRules()
        {
            return ConversationalRuleHandler.FindAllConversationalRules();
        }

        // Show all pending rules stored in the database
        public List<Rule> ShowAllPendingRules()
        {
            List<FixedConversationalRule> fCRulesList = ShowAllFixedConversationalRuleRules();
            List<ConversationalRule> cRulesList = ShowAllConversationalRuleRules();
            List<Rule> pendingRulesList = new List<Rule>();
            foreach (FixedConversationalRule fCRule in fCRulesList)
            {
                if (fCRule.Status == Status.Pending.ToString())
                {
                    pendingRulesList.Add(fCRule);
                }
            }
            foreach (ConversationalRule cRule in cRulesList)
            {
                if (cRule.Status == Status.Pending.ToString())
                {
                    pendingRulesList.Add(cRule);
                }
            }
            return pendingRulesList;
        }

        // Show all rejected rules stored in the database
        public List<Rule> ShowAllRejectedRules()
        {
            List<FixedConversationalRule> fCRulesList = ShowAllFixedConversationalRuleRules();
            List<ConversationalRule> cRulesList = ShowAllConversationalRuleRules();
            List<Rule> rejectedRulesList = new List<Rule>();
            foreach (FixedConversationalRule fCRule in fCRulesList)
            {
                if (fCRule.Status == Status.Rejected.ToString())
                {
                    rejectedRulesList.Add(fCRule);
                }
            }
            foreach (ConversationalRule cRule in cRulesList)
            {
                if (cRule.Status == Status.Rejected.ToString())
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
        public void EditPendingRule(string userId, string ruleId, string ruleInput, string ruleOutput)
        {
            string relatedUserId = userId;
            Rule rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(ruleId);
            List<FixedConversationalRule> fCRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            List<ConversationalRule> cRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            if (rule != null)
            {
                for (int i = 0; i < fCRulesList.Count; i++)
                {
                    if (fCRulesList[i].Id == int.Parse(ruleId))
                    {
                        relatedUserId = (StringFormat(fCRulesList[i].RelatedUsersId,userId) + " " + userId).Trim();
                        break;
                    }
                }
                FixedConversationalRule fRule = new FixedConversationalRule
                {
                    Input = ruleInput,
                    Output = ruleOutput,
                    RelatedUsersId = userId,
                    Status = "Pending"
                };
                FixedConversationalRuleHandler.UpdateAFixedConversationalRule(fRule);
            }
            else
            {
                rule = ConversationalRuleHandler.FindConversationalRuleById(ruleId);
                if (rule != null)
                {
                    for (int i = 0; i < cRulesList.Count; i++)
                    {
                        if (cRulesList[i].Id == int.Parse(ruleId))
                        {
                            relatedUserId = (StringFormat(cRulesList[i].RelatedUsersId, userId) + " " + userId).Trim();
                            break;
                        }
                    }
                    ConversationalRule cRule = new ConversationalRule
                    {
                        Input = ruleInput,
                        Output = ruleOutput,
                        RelatedUsersId = userId,
                        Status = "Pending"
                    };
                    ConversationalRuleHandler.UpdateAConversationalRule(cRule);
                }
            }
        }

        // Delete a rule
        public void DeletePendingRule(string ruleId)
        {
            Rule rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(ruleId);
            if (rule != null)
            {
                FixedConversationalRuleHandler.RemoveFixedConversationalRule(ruleId);
            }
            else
            {
                rule = ConversationalRuleHandler.FindConversationalRuleById(ruleId);
                if (rule != null)
                {
                    ConversationalRuleHandler.RemoveConversationalRule(ruleId);
                }
            }
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
            string az = "qwertyuiopasdfghjklzxcvbnm ";
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
            //if (input != null && output != null && !input.Contains("{") && !input.Contains("}") && !output.Contains("{") && !output.Contains("}"))
            //{
            //    input = input.ToLower();
            //    foreach(char x in input)
            //    {
            //        if(!az.Contains(x) && !num.Contains(x))
            //        {
            //            return false;
            //        }
            //    }
            //    output = output.ToLower();
            //    foreach (char x in output)
            //    {
            //        if (!az.Contains(x) && !num.Contains(x))
            //        {
            //            return false;
            //        }
            //    }
            //    return true;
            //}
            //return false;
        }

        // Make sure the input only contains one coloum
        public bool IsRuleValid(string input)
        {
            //string compactedString = IgnoreWhiteSpace(input);
            string az = "qwertyuiopasdfghjklzxcvbnm ";
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
            //if (input != null)
            //{
            //    input = input.ToLower();
            //    foreach (char x in input)
            //    {
            //        if (!az.Contains(x) && !num.Contains(x) && !marks.Contains(x))
            //        {
            //            return false;
            //        }
            //    }
            //    string[] phrase1;
            //    string[] phrase2;
            //    phrase1 = input.Split('{');
            //    phrase2 = input.Split('}');
            //    string[] words1;
            //    string[] words2;
            //    if (phrase1.Count() > 1 && phrase2.Count() > 1)
            //    {
            //        words1 = phrase1[1].Split(' ');
            //        words2 = phrase2[0].Split(' ');
            //        if (words1.Count() > 1 && words2.Count() > 1 && words1[1] == words2[words2.Count() - 2] && phrase1.Count() == 2 && phrase2.Count() == 2 && (words1[1] == "topic" ||
            //                words1[1] == "participants" || words1[1] == "location" || words1[1] == "startdate" || words1[1] == "enddate"))
            //        {
            //            return true;
            //        }
            //    }

            //    return false;
            //}
            //else
            //{
            //    return false;
            //}
        }
        
       public Rule FindRuleById(string id)
        {
            Rule rule = FixedConversationalRuleHandler.FindFixedConversationalRuleById(id);
            if (rule != null)
            {
                return rule;
            }
            else
            {
                rule = ConversationalRuleHandler.FindConversationalRuleById(id);
            }
            return rule;
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