using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class EditorService
    {

        public List<FixedConversationalRule> AddNewFCRule(string input, string output, string userId, List<FixedConversationalRule> fCRulesList)
        {
            FixedConversationalRule rule = new FixedConversationalRule(Utils.CreateIdByType("FixedConversationalRule", fCRulesList), input, output, userId, Status.Pending);
            //bool repeated = false;
            //foreach (FixedConversationalRule r in fCRulesList)
            //{
            //    if (rule.Input == r.Input)
            //    {
            //        repeated = true;
            //    }
            //}
            //if (!repeated)
            //{
                fCRulesList.Add(rule);
            //}
            
            return fCRulesList;
        }

        public List<ConversationalRule> AddNewCRule(string input, string output, string userId, List<ConversationalRule> cRulesList)
        {
            ConversationalRule rule = new ConversationalRule(Utils.CreateIdByType("ConversationalRule", cRulesList), input, output, userId, Status.Pending);
            cRulesList.Add(rule);
            return cRulesList;
        }

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

        //public List<FixedConversationalRule> ShowAllPendingFCRules(List<FixedConversationalRule> fixedConversationalRulesList)
        //{
        //    List<FixedConversationalRule> pendingFCRulesList = new List<FixedConversationalRule>();
        //    foreach(FixedConversationalRule fCRule in fixedConversationalRulesList)
        //    {
        //        if(fCRule.Status == Status.Pending)
        //        {
        //            pendingFCRulesList.Add(fCRule);
        //        }
        //    }
        //    return pendingFCRulesList;
        //}

        //public List<ConversationalRule> ShowAllPendingCRules(List<ConversationalRule> conversationalRulesList)
        //{
        //    List<ConversationalRule> pendingCRulesList = new List<ConversationalRule>();
        //    foreach(ConversationalRule cRule in conversationalRulesList)
        //    {
        //        if(cRule.Status == Status.Pending)
        //        {
        //            pendingCRulesList.Add(cRule);
        //        }
        //    }
        //    return pendingCRulesList;
        //}

        //public List<FixedConversationalRule> ShowAllRejectedFCRules(List<FixedConversationalRule> fixedConversationalRulesList)
        //{
        //    List<FixedConversationalRule> rejectedFCRulesList = new List<FixedConversationalRule>();
        //    foreach (FixedConversationalRule fCRule in fixedConversationalRulesList)
        //    {
        //        if (fCRule.Status == Status.Rejected)
        //        {
        //            rejectedFCRulesList.Add(fCRule);
        //        }
        //    }
        //    return rejectedFCRulesList;
        //}

        //public List<ConversationalRule> ShowAllRejectedCRules(List<ConversationalRule> conversationalRulesList)
        //{
        //    List<ConversationalRule> rejectedCRulesList = new List<ConversationalRule>();
        //    foreach (ConversationalRule cRule in conversationalRulesList)
        //    {
        //        if (cRule.Status == Status.Rejected)
        //        {
        //            rejectedCRulesList.Add(cRule);
        //        }
        //    }
        //    return rejectedCRulesList;
        //}

        //Not sure about input and output format
        public Tuple<List<FixedConversationalRule>, List<ConversationalRule>> EditPendingRule(string userId, string ruleId, string ruleInput, string ruleOutput, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            bool valueChanged = false;
            for(int i=0; i<fCRulesList.Count; i++)
            {
                if(fCRulesList[i].Id == ruleId)
                {
                    fCRulesList[i].Input = ruleInput;
                    fCRulesList[i].Output = ruleOutput;
                    fCRulesList[i].LastRelatedUserID += " " + userId;
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
                        break;
                    }
                }
            }
            //FixedConversationalRule fCRule = FindFCRule(ruleId, fixedConversationalRulesList);
            //fCRule.Input = ruleInput;
            //fCRule.Output = ruleOutput;
            //DeletePendingFCRule(ruleId, ref fixedConversationalRulesList);
            //fixedConversationalRulesList.Add(fCRule);
            return Tuple.Create(fCRulesList, cRulesList);
        }

        //Not sure about input and output format
        //public void EditPendingCRule(string ruleId, string ruleInput, string ruleOutput, ref List<ConversationalRule> conversationalRulesList)
        //{
        //    ConversationalRule cRule = FindCRule(ruleId, conversationalRulesList);
        //    cRule.Input = ruleInput;
        //    cRule.Output = ruleOutput;
        //    DeletePendingCRule(ruleId, ref conversationalRulesList);
        //    conversationalRulesList.Add(cRule);
        //}

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

        //public void DeletePendingCRule(string ruleId, ref List<ConversationalRule> conversationalRulesList)
        //{
        //    ConversationalRule rule = FindCRule(ruleId, conversationalRulesList);
        //    if (rule != null)
        //    {
        //        conversationalRulesList.Remove(rule);
        //    }
        //}

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

        //public List<ConversationalRule> ShowCurrentUserApprovedCRules(User user)
        //{
        //    return null;
        //}

        public int ShowCurrentUserApprovedRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserApprovedRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        public int ShowCurrentUserRejectedRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserRejectedRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        public int ShowCurrentUserPendingRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserPendingRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

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

        public bool IsValidInput (string input, string output)
        {
            if (!input.Contains("{") && !input.Contains("}") && !output.Contains("{") && !output.Contains("}"))
            {
                return true;
            }
            else if (CheckInput(input) && CheckInput(output))
            {
                return true;
            }
            return false;
        }

        private bool CheckInput (string input)
        {
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

        public bool CheckRepeatingRule(string input, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            char[] WhiteSpace = new char[] { ' ' };
            string longString = input;
            string[] split = longString.Split(WhiteSpace, StringSplitOptions.RemoveEmptyEntries);
            string compactedString = string.Join(" ", split);

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

//foreach(Rule rule in rulesList)
//{
//    ConversationalRule cr = rule as ConversationalRule;
//    if(cr != null)
//    {
//        if (cr.Id == ruleId)
//        {
//            return cr;
//        }
//    }
//    else{
//        FixedConversationalRule fr = rule as FixedConversationalRule;
//        if (fr.Id == ruleId)
//        {
//            return fr;
//        }
//    }
//}