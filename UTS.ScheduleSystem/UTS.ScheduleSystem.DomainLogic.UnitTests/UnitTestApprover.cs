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

        [TestMethod]
        public void ApproverService_RequestPendingFixedConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1); // pending
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            List<FixedConversationalRule> pendingFixedConversationalRuleList = approverService.RequestPendingFixedConversationalRulesList();
            foreach (FixedConversationalRule fixedConversationalRule in pendingFixedConversationalRuleList)
            {
                Assert.AreEqual(Status.Pending.ToString(), fixedConversationalRule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestRejectedConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1); 
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3); // Rejected
            List<ConversationalRule> rejectedConversationalRuleList = approverService.RequestRejectedConversationalRulesList();
            foreach (ConversationalRule conversationalRule in rejectedConversationalRuleList)
            {
                Assert.AreEqual(Status.Rejected.ToString(), conversationalRule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestRejectedFixedConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3); // Rejected
            List<FixedConversationalRule> rejectedFixedConversationalRuleList = approverService.RequestRejectedFixedConversationalRulesList();
            foreach (FixedConversationalRule fixedConversationalRule in rejectedFixedConversationalRuleList)
            {
                Assert.AreEqual(Status.Rejected.ToString(), fixedConversationalRule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestApprovedConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2); // Approved
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            List<ConversationalRule> approvedConversationalRuleList = approverService.RequestApprovedConversationalRulesList();
            foreach (ConversationalRule conversationalRule in approvedConversationalRuleList)
            {
                Assert.AreEqual(Status.Approved.ToString(), conversationalRule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestApprovedFixedConversationalRulesList_ReturenCorrectPendingRuleList()
        {
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2); // Approved
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            List<FixedConversationalRule> approvedFixedConversationalRuleList = approverService.RequestApprovedFixedConversationalRulesList();
            foreach (FixedConversationalRule fixedConversationalRule in approvedFixedConversationalRuleList)
            {
                Assert.AreEqual(Status.Approved.ToString(), fixedConversationalRule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_ApproveRule_CorrectApprovedRules()
        {
            // Copy rules to database
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);

            // Change status
            approverService.ApproveConversationalRule("1");
            approverService.ApproveFixedConversationalRule("1");

            // Get rules from database
            ConversationalRule testCRule = ConversationalRuleHandler.FindConversationalRuleById("1");
            FixedConversationalRule testFCRule = FixedConversationalRuleHandler.FindFixedConversationalRuleById("1");

            Assert.AreEqual(Status.Approved.ToString(), testCRule.Status);
            Assert.AreEqual(Status.Approved.ToString(), testFCRule.Status);
        }

        [TestMethod]
        public void ApproverService_RejectRule_CorrectRejectedRules()
        {
            // Copy rules to database
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);

            // Change status
            approverService.RejectRuleInConversationalRuleList("1");
            approverService.RejectRuleInFixedConversationalRuleList("1");

            // Get rules from database
            ConversationalRule testCRule = ConversationalRuleHandler.FindConversationalRuleById("1");
            FixedConversationalRule testFCRule = FixedConversationalRuleHandler.FindFixedConversationalRuleById("1");

            Assert.AreEqual(Status.Rejected.ToString(), testCRule.Status);
            Assert.AreEqual(Status.Rejected.ToString(), testFCRule.Status);
        }

        [TestMethod]
        public void ApproverService_ApprovedRulesNum_ReturnCorrectNumberOfApprovedRules()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            int approvedRuleNum = approverService.ApprovedRulesNum();
            Assert.AreEqual(2, approvedRuleNum);
        }

        [TestMethod]
        public void ApproverService_RejectedRulesNum_ReturnCorrectNumberOfRejectedRules()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            int rejectedRuleNum = approverService.RejectedRulesNum();
            Assert.AreEqual(2, rejectedRuleNum);
        }

        [TestMethod]
        public void ApproverService_SuccessRate_ReturnCorrectSuccessRate()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            double successRate = approverService.SuccessRate();
            Assert.AreEqual(50, successRate);
        }

        [TestMethod]
        public void ApproverService_UserRelatedApprovedRulesNum_ReturnCorrectNumberOfApprovedRulesByUser()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            int UserRelatedApprovedRulesNum = approverService.UserRelatedApprovedRulesNum(UnitTestPublic.testUserId1);
            Assert.AreEqual(2, UserRelatedApprovedRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserRelatedRejectedRulesNum_ReturnCorrectNumberOfRejectedRulesByUser()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            int UserRelatedRejectedRulesNum = approverService.UserRelatedRejectedRulesNum(UnitTestPublic.testUserId1);
            Assert.AreEqual(2, UserRelatedRejectedRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserRelatedPendingRulesNum_ReturnCorrectNumberOfPendingRulesByUser()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            int UserRelatedPendingRulesNum = approverService.UserRelatedPendingRulesNum(UnitTestPublic.testUserId1);
            Assert.AreEqual(2, UserRelatedPendingRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserSuccessRate_ReturnCorrectSuccessRateOfUser()
        {
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule1);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule2);
            ConversationalRuleHandler.AddConversationalRule(UnitTestPublic.cRule3);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule1);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule2);
            FixedConversationalRuleHandler.AddFixedConversationalRule(UnitTestPublic.cFRule3);
            double userSuccessRate = approverService.UserSuccessRate(UnitTestPublic.testUserId1);
            Assert.AreEqual(0.5, userSuccessRate);
        }
    }
}
