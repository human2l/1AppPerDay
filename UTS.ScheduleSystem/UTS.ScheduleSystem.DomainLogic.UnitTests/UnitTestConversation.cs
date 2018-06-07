using System;
using UTS.ScheduleSystem.Data;
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
            ConversationalRule testConversationalRule = UnitTestPublic.cRule2;
            testConversationalRule.Input = testConversationalRule.Input.ToLower();
            testConversationalRule.Output = testConversationalRule.Output.ToLower();

            FixedConversationalRule testFixedConversationalRule = UnitTestPublic.cFRule2;
            testFixedConversationalRule.Input = testFixedConversationalRule.Input.ToLower();
            testFixedConversationalRule.Output = testFixedConversationalRule.Output.ToLower();

            ConversationalRuleHandler.AddConversationalRule(testConversationalRule);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);

            string expectedFixedRuleAnswer = "i am fine";
            string expectedRuleAnswer = "it is a blah";

            string testFixedRuleAnswer = conversationService.Conversation("How are you".ToLower());
            string testRuleAnswer = conversationService.Conversation("When will I have meal with a blah".ToLower());

            Assert.AreEqual(expectedFixedRuleAnswer, testFixedRuleAnswer);
            Assert.AreEqual(expectedRuleAnswer, testRuleAnswer);
        }
    }
}
