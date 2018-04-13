﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class EditorService
    {

        public void AddNewFCRule(FixedConversationalRule rule, ref List<FixedConversationalRule> fCRulesList)
        {
            fCRulesList.Add(rule);
        }

        public void AddNewCRule(ConversationalRule rule, ref List<ConversationalRule> cRulesList)
        {
            cRulesList.Add(rule);
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

        //public List<FixedConversationalRule> ShowAllPendingFCRules(List<FixedConversationalRule> fCRulesList)
        //{
        //    List<FixedConversationalRule> pendingFCRulesList = new List<FixedConversationalRule>();
        //    foreach(FixedConversationalRule fCRule in fCRulesList)
        //    {
        //        if(fCRule.Status == Status.Pending)
        //        {
        //            pendingFCRulesList.Add(fCRule);
        //        }
        //    }
        //    return pendingFCRulesList;
        //}

        //public List<ConversationalRule> ShowAllPendingCRules(List<ConversationalRule> cRulesList)
        //{
        //    List<ConversationalRule> pendingCRulesList = new List<ConversationalRule>();
        //    foreach(ConversationalRule cRule in cRulesList)
        //    {
        //        if(cRule.Status == Status.Pending)
        //        {
        //            pendingCRulesList.Add(cRule);
        //        }
        //    }
        //    return pendingCRulesList;
        //}

        //public List<FixedConversationalRule> ShowAllRejectedFCRules(List<FixedConversationalRule> fCRulesList)
        //{
        //    List<FixedConversationalRule> rejectedFCRulesList = new List<FixedConversationalRule>();
        //    foreach (FixedConversationalRule fCRule in fCRulesList)
        //    {
        //        if (fCRule.Status == Status.Rejected)
        //        {
        //            rejectedFCRulesList.Add(fCRule);
        //        }
        //    }
        //    return rejectedFCRulesList;
        //}

        //public List<ConversationalRule> ShowAllRejectedCRules(List<ConversationalRule> cRulesList)
        //{
        //    List<ConversationalRule> rejectedCRulesList = new List<ConversationalRule>();
        //    foreach (ConversationalRule cRule in cRulesList)
        //    {
        //        if (cRule.Status == Status.Rejected)
        //        {
        //            rejectedCRulesList.Add(cRule);
        //        }
        //    }
        //    return rejectedCRulesList;
        //}

        //Not sure about input and output format
        public void EditPendingRule(string ruleId, string ruleInput, string ruleOutput, ref List<FixedConversationalRule> fCRulesList, ref List<ConversationalRule> cRulesList)
        {
            bool valueChanged = false;
            for(int i=0; i<fCRulesList.Count; i++)
            {
                if(fCRulesList[i].Id == ruleId)
                {
                    fCRulesList[i].Input = ruleInput;
                    fCRulesList[i].Output = ruleOutput;
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
            //FixedConversationalRule fCRule = FindFCRule(ruleId, fCRulesList);
            //fCRule.Input = ruleInput;
            //fCRule.Output = ruleOutput;
            //DeletePendingFCRule(ruleId, ref fCRulesList);
            //fCRulesList.Add(fCRule);
        }

        //Not sure about input and output format
        //public void EditPendingCRule(string ruleId, string ruleInput, string ruleOutput, ref List<ConversationalRule> cRulesList)
        //{
        //    ConversationalRule cRule = FindCRule(ruleId, cRulesList);
        //    cRule.Input = ruleInput;
        //    cRule.Output = ruleOutput;
        //    DeletePendingCRule(ruleId, ref cRulesList);
        //    cRulesList.Add(cRule);
        //}

        public void DeletePendingRule(string ruleId, ref List<FixedConversationalRule> fCRulesList, ref List<ConversationalRule> cRulesList)
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
        }

        //public void DeletePendingCRule(string ruleId, ref List<ConversationalRule> cRulesList)
        //{
        //    ConversationalRule rule = FindCRule(ruleId, cRulesList);
        //    if (rule != null)
        //    {
        //        cRulesList.Remove(rule);
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

        //public List<ConversationalRule> ShowCurrentUserApprovedCRules(User user)
        //{
        //    return null;
        //}

        public int ShowCurrentUserApprovedRulesCount(User user, List<FixedConversationalRule> fCRulesList, List<ConversationalRule> cRulesList)
        {
            int count = ShowCurrentUserApprovedRules(user, fCRulesList, cRulesList).Count;
            return count;
        }

        //public int ShowCurrentUserRejectedRulesCount(User user)
        //{
        //    int count = 0;
        //    return count;
        //}

        public double ShowCurrentUserSuccessRate(User user)
        {
            double rate = 0;
            return rate;
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