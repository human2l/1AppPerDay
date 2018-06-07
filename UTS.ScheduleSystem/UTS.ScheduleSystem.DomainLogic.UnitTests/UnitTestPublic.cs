using System;
using System.Collections.Generic;
using System.IO;
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
        public static ConversationalRule cRule11 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };
        public static ConversationalRule cRule2 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Approved.ToString()
        };
        public static ConversationalRule cRule21 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };
        public static ConversationalRule cRule3 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Rejected.ToString()
        };
        public static ConversationalRule cRule31 = new ConversationalRule
        {
            Input = "When will I have meal with { topic } blah",
            Output = "It's { topic } blah",
            Status = Status.Pending.ToString()
        };

        public static FixedConversationalRule cFRule1 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cFRule11 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cFRule2 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Approved.ToString()
        };
        public static FixedConversationalRule cFRule21 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Pending.ToString()
        };
        public static FixedConversationalRule cFRule3 = new FixedConversationalRule
        {
            Input = "How are you?",
            Output = "I'm fine, thanks, and you?",
            Status = Status.Rejected.ToString()
        };
        public static FixedConversationalRule cFRule31 = new FixedConversationalRule
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
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\UTS.ScheduleSystem.Data"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            tempFixedConversationalRulesList = FixedConversationalRuleHandler.FindAllFixedConversationalRules();
            tempConversationalRulesList = ConversationalRuleHandler.FindAllConversationalRules();
            tempMealScheduleList = MealScheduleHandler.FindAllMealSchedules();
        }

        // Restore the database from backup
        public static void TerminateAllTest()
        {
            foreach (FixedConversationalRule fcRule in tempFixedConversationalRulesList)
            {
                FixedConversationalRuleHandler.AddFixedConversationalRule(fcRule);
            }

            foreach (ConversationalRule cRule in tempConversationalRulesList)
            {
                ConversationalRuleHandler.AddConversationalRule(cRule);
            }

            foreach (MealSchedule m in tempMealScheduleList)
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
