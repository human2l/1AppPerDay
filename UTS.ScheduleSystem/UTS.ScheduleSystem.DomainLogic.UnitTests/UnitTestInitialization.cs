using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    class UnitTestInitialization
    {
        public static void Initial()
        {
            ConversationalRule cRule1 = new ConversationalRule("When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Pending.ToString());
            ConversationalRule crule11 = new ConversationalRule("when will i have meal with { topic } blah", "it's { topic } blah", "u001 u002", Status.Pending.ToString());
            ConversationalRule crule2 = new ConversationalRule("when will i have meal with { topic } blah", "it's { topic } blah", "u001 u002", Status.Approved.ToString());
            ConversationalRule crule21 = new ConversationalRule("when will i have meal with { topic } blah", "it's { topic } blah", "u001 u002", Status.Pending.ToString());
            ConversationalRule crule3 = new ConversationalRule("when will i have meal with { topic } blah", "it's { topic } blah", "u001 u002", Status.Rejected.ToString());
            ConversationalRule crule31 = new ConversationalRule("when will i have meal with { topic } blah", "{p1}", "u001 u002", Status.Pending.ToString());
            FixedConversationalRule cfrule1 = new FixedConversationalRule("how do you do", "i'm fine, fuck you, and you?", "u001", Status.Pending.ToString());
            FixedConversationalRule cfrule11 = new FixedConversationalRule("how do you do", "i'm fine, fuck you, and you?", "u001", Status.Pending.ToString());
            FixedConversationalRule cfrule2 = new FixedConversationalRule("what is your name", "are you flirting with me?", "u001", Status.Approved.ToString());
            FixedConversationalRule cfrule21 = new FixedConversationalRule("what is your name", "are you flirting with me?", "u001", Status.Pending.ToString());
            FixedConversationalRule cfrule3 = new FixedConversationalRule("i'm not good", "so go fuck yourself", "u001", Status.Rejected.ToString());
            FixedConversationalRule cfrule31 = new FixedConversationalRule("i'm not good", "so go fuck yourself", "u001", Status.Pending.ToString());
            MealSchedule ms1 = new MealSchedule
            {
                Topic = "a",
                Participants = "b",
                Location = "c",
                StartDate = "d",
                EndDate = "e",
            };
            MealSchedule ms2 = new MealSchedule
            {
                Topic = "f",
                Participants = "g",
                Location = "h",
                StartDate = "i",
                EndDate = "j",
            };
            MealSchedule ms3 = new MealSchedule
            {
                Topic = "k",
                Participants = "l",
                Location = "m",
                StartDate = "n",
                EndDate = "o",
            };
        }

    }
}
