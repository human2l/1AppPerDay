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
                Assert.AreEqual(Status.Pending.ToString(), conversationalRule.Status);
            }
        }

        //[TestMethod]
        //public void ApproverService_RequestPendingFixedConversationalRulesList_ReturenCorrectPendingRuleList()
        //{
        //    FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cRule1); // pending
        //    FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cRule2);
        //    FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cRule3);
        //    List<FixedConversationalRule> pendingFixedConversationalRuleList = approverService.RequestPendingFixedConversationalRulesList();
        //    foreach (FixedConversationalRule fixedConversationalRule in pendingFixedConversationalRuleList)
        //    {
        //        Assert.AreEqual(Status.Pending.ToString(), fixedConversationalRule.Status);
        //    }
        //}
    }
}
