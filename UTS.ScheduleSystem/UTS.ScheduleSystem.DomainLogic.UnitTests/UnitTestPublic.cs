using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    class UnitTestPublic
    {
        public static List<FixedConversationalRule> tempFixedConversationalRulesList = new List<FixedConversationalRule>();
        public static List<ConversationalRule> tempConversationalRulesList = new List<ConversationalRule>();
        public static List<MealSchedule> tempMealScheduleList = new List<MealSchedule>();

        public static ConversationalRule cRule1 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };
        public static ConversationalRule crule11 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };
        public static ConversationalRule crule2 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Approved.ToString()
        };
        public static ConversationalRule crule21 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };
        public static ConversationalRule crule3 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Rejected.ToString()
        };
        public static ConversationalRule crule31 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };

        public static FixedConversationalRule cfrule1 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cfrule11 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cfrule2 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Approved.ToString()
        };
        public static FixedConversationalRule cfrule21 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cfrule3 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Rejected.ToString()
        };
        public static FixedConversationalRule cfrule31 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static MealSchedule ms1 = new MealSchedule
        {
            Topic = "a",
            Participants = "b",
            Location = "c",
            StartDate = "d",
            EndDate = "e",
        };
        public static MealSchedule ms2 = new MealSchedule
        {
            Topic = "f",
            Participants = "g",
            Location = "h",
            StartDate = "i",
            EndDate = "j",
        };
        public static MealSchedule ms3 = new MealSchedule
        {
            Topic = "k",
            Participants = "l",
            Location = "m",
            StartDate = "n",
            EndDate = "o",
        };

        //Backup the database into memory
        public static void StartAllTest()
        {
            UnitTestPublic.tempFixedConversationalRulesList = FixedConversationalRuleHandler.FindAllApprovedFixedConversationalRules();
            UnitTestPublic.tempConversationalRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            UnitTestPublic.tempMealScheduleList = MealScheduleHandler.FindAllMealSchedules();
        }

        // Restore the database from backup
        public static void TerminateAllTest()
        {
            foreach (FixedConversationalRule fcRule in UnitTestPublic.tempFixedConversationalRulesList)
            {
                FixedConversationalRuleHandler.AddFixedConversationalRule(fcRule);
            }

            foreach (ConversationalRule cRule in UnitTestPublic.tempConversationalRulesList)
            {
                ConversationalRuleHandler.AddConversationalRule(cRule);
            }

            foreach (MealSchedule m in UnitTestPublic.tempMealScheduleList)
            {
                MealScheduleHandler.AddMealschedule(m);
            }
        }

        // Clear database
        public static void Clear()
        {
            FixedConversationalRuleHandler.ClearAllFixedConversationalRule();
            ConversationalRuleHandler.ClearAllConversationalRule();
            MealScheduleHandler.ClearAllMealSchedule();
        }

        // Compare two rule to see if they are the same one
        public static Boolean CompareTwoRules(Rule rule1, Rule rule2)
        {
            Boolean isSame = (rule1.Input.Equals(rule2.Input) &&
                rule1.Output.Equals(rule2.Output) &&
                rule1.RelatedUsersId.Equals(rule2.RelatedUsersId) &&
                rule1.Status.Equals(rule2.Status)) ? true : false;
            return isSame;
        }

    }
}
