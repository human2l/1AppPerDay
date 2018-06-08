using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

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
        public void EditorService_CheckRepeatingRule()
        {
            editorService.AddNewFCRule(UnitTestPublic.cFRule1.Input, UnitTestPublic.cFRule1.Output, UnitTestPublic.cFRule1.RelatedUsersId);

            Assert.AreEqual(true, editorService.CheckRepeatingRule(UnitTestPublic.cFRule1.Input));
        }

        [TestMethod]
        public void EditorService_EditRule()
        {
            editorService.AddNewFCRule(UnitTestPublic.cFRule1.Input, UnitTestPublic.cFRule1.Output, UnitTestPublic.cFRule1.RelatedUsersId);
            FixedConversationalRule editedRule = new FixedConversationalRule();

            List<FixedConversationalRule> testFcRuleList = new List<FixedConversationalRule>();
            testFcRuleList = editorService.ShowAllFixedConversationalRuleRules();
            FixedConversationalRule testFcRule1 = new FixedConversationalRule();
            if (testFcRuleList.Count == 1)
            {
                testFcRule1 = testFcRuleList[0];
            }
            editedRule = testFcRule1;
            editedRule.Input = "edited";
            testFcRule1.Input = "edited";
            FixedConversationalRuleHandler.UpdateAFixedConversationalRule(editedRule);

            testFcRuleList = editorService.ShowAllFixedConversationalRuleRules();
            if (testFcRuleList.Count == 1)
            {
                testFcRule1 = testFcRuleList[0];
            }

            Assert.IsTrue(UnitTestPublic.CompareTwoFcRules(editedRule, testFcRule1));
        }

        [TestMethod]
        public void EditorService_RemoveRule()
        {
            editorService.AddNewFCRule(UnitTestPublic.cFRule1.Input, UnitTestPublic.cFRule1.Output, UnitTestPublic.cFRule1.RelatedUsersId);
            editorService.AddNewFCRule(UnitTestPublic.cFRule2.Input, UnitTestPublic.cFRule2.Output, UnitTestPublic.cFRule2.RelatedUsersId);

            List<FixedConversationalRule> testFcRuleList = new List<FixedConversationalRule>();
            testFcRuleList = editorService.ShowAllFixedConversationalRuleRules();
            FixedConversationalRule testFcRule1 = new FixedConversationalRule();
            FixedConversationalRule testFcRule2 = new FixedConversationalRule();
            if (testFcRuleList.Count == 2)
            {
                testFcRule1 = testFcRuleList[0];
                testFcRule2 = testFcRuleList[1];
            }
            FixedConversationalRuleHandler.RemoveFixedConversationalRule(testFcRule1.Id + "");
            testFcRuleList = editorService.ShowAllFixedConversationalRuleRules();
            if (testFcRuleList.Count == 1)
            {
                testFcRule1 = testFcRuleList[0];
            }
            Assert.IsTrue(UnitTestPublic.CompareTwoFcRules(testFcRule1, testFcRule2));
        }

    }
}
