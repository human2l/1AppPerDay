using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestConversation
    {
        ConversationService conversationService = new ConversationService();

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

        // Restore the database from backup
        [ClassCleanup]
        public static void TerminateAllTest()
        {
            UnitTestPublic.TerminateAllTest();
        }

        [TestMethod]
        public void ConversationService_Conversation_GetCorrectAnswer()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);

            string expectedFixedRuleAnswer = "I am fine";
            string expectedRuleAnswer = "It is a blah";

            string testFixedRuleAnswer = conversationService.Conversation("How are you");
            string testRuleAnswer = conversationService.Conversation("When will I have meal with a blah");

            Assert.AreEqual(expectedFixedRuleAnswer, testFixedRuleAnswer);
            Assert.AreEqual(expectedRuleAnswer, testRuleAnswer);
        }
    }
}
