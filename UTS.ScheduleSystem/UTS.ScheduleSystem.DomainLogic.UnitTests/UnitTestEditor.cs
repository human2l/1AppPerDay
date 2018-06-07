using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestEditor
    {
        private static EditorService editorService = new EditorService();

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
        public void EditorService_AddFixedConversationalRule()
        {
            // Adding a fixed conversationalRule
            editorService.AddNewFCRule(UnitTestPublic.cFRule1.Input, UnitTestPublic.cFRule1.Output, UnitTestPublic.cFRule1.RelatedUsersId);
            List<FixedConversationalRule> testFcRuleList = new List<FixedConversationalRule>();
            testFcRuleList = editorService.ShowAllFixedConversationalRuleRules();
            FixedConversationalRule testFcRule1 = new FixedConversationalRule();
            if (testFcRuleList.Count == 1)
            {
                testFcRule1 = testFcRuleList[0];
            }

            Assert.IsTrue(UnitTestPublic.CompareTwoFcRules(UnitTestPublic.cFRule1, testFcRule1));
        }

        [TestMethod]
        public void EditorService_AddConversationalRule()
        {
            // Adding a conversationalRule
            editorService.AddNewCRule(UnitTestPublic.cRule1.Input, UnitTestPublic.cRule1.Output, UnitTestPublic.cRule1.RelatedUsersId);
            List<ConversationalRule> testCRuleList = new List<ConversationalRule>();
            testCRuleList = editorService.ShowAllConversationalRuleRules();
            ConversationalRule testCRule1 = new ConversationalRule();
            if (testCRuleList.Count == 1)
            {
                testCRule1 = testCRuleList[0];
            }

            Assert.IsTrue(UnitTestPublic.CompareTwoCRules(testCRule1, testCRule1));
        }

        [TestMethod]
        public void EditorService_ShowAllPendingRules()
        {
            editorService.AddNewFCRule(UnitTestPublic.cFRule1.Input, UnitTestPublic.cFRule1.Output, UnitTestPublic.cFRule1.RelatedUsersId);
            editorService.AddNewCRule(UnitTestPublic.cRule1.Input, UnitTestPublic.cRule1.Output, UnitTestPublic.cRule1.RelatedUsersId);
            List<Rule> testPendingRulesList = new List<Rule>();
            testPendingRulesList.Add(UnitTestPublic.cFRule1);
            testPendingRulesList.Add(UnitTestPublic.cRule1);
            List<Rule> pendingRulesList = new List<Rule>();
            pendingRulesList = editorService.ShowAllPendingRules();
            foreach (Rule rule in testPendingRulesList)
            {

            }

        }

    }
}
