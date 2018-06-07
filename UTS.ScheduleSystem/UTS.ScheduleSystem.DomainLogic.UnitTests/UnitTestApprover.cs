using System;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic;
using UTS.ScheduleSystem.DomainLogic.DataHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestApprover
    {
        private ApproverService approverService = new ApproverService();

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
        public void ApproverService_RequestPendingConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1); // pending
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            List<ConversationalRule> pendingConversationalRuleList = approverService.RequestPendingConversationalRulesList();
            foreach (ConversationalRule conversationalRule in pendingConversationalRuleList)
            {
                Debug.WriteLine(conversationalRule.Id);
                Debug.WriteLine(conversationalRule.Input);
                Debug.WriteLine(conversationalRule.Output);
                Debug.WriteLine(conversationalRule.Status);
                Assert.AreEqual(Status.Pending.ToString(), conversationalRule.Status);
            }
        }
    }
}
