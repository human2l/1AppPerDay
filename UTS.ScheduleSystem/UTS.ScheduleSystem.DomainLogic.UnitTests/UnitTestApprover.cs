using System;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic;
using UTS.ScheduleSystem.DomainLogic.DataHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestApprover
    {
        [ClassInitialize]
        public static void StartAllTest(TestContext testContext)
        {
            //Backup the database into memory
            UnitTestPublic.StartAllTest();
        }

        [TestInitialize]
        public void StartTest()
        {
            // Empty the database
            UnitTestPublic.Clear();
        }

        [TestCleanup()]
        public void TerminateTest()
        {
            // Empty the database
            UnitTestPublic.Clear();
        }

        [ClassCleanup]
        public static void TerminateAllTest()
        {
            // Restore the database from backup
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

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
