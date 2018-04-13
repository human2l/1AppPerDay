using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UTS.ScheduleSystem.UnitTesting
{
    [TestClass]
    public class UnitTests
    {
        [TestClass]
        public class UnitTest
        {
            Controller controller = new Controller();
            //controller = new Controller();
            //controller.initialization();
            User frank = new User("u002", "FRANK", "wtf", "FRANK@wtf.com", Role.DMnEnA);
            ConversationalRule cRule1 = new ConversationalRule("c001", "When will I have meal with {p1}", "It's {p1}", "u001 u002", Status.Pending);
            ConversationalRule cRule2 = new ConversationalRule("c001", "Who will I have meal with on {p1}", "It's {p1}", "u001 u002", Status.Approved);
            ConversationalRule cRule3 = new ConversationalRule("c001", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            FixedConversationalRule cFRule1 = new FixedConversationalRule("fc002", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            FixedConversationalRule cFRule2 = new FixedConversationalRule("fc002", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
            FixedConversationalRule cFRule3 = new FixedConversationalRule("fc002", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            [TestInitialize]
            public void Setup()
            {
                
                controller.CRulesList.Add(cRule1);
                controller.CRulesList.Add(cRule2);
                controller.CRulesList.Add(cRule3);
                controller.FCRulesList.Add(cFRule1);
                controller.FCRulesList.Add(cFRule2);
                controller.FCRulesList.Add(cFRule3);
            }
            //[TestMethod]
            //public void ControllerLists_OnInitialization_NotNull()
            //{
            //    //print controller
            //}

            [TestMethod]

            public void ApproverService_RequestPendingRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule1);
                
                correctPRulesList.Add(cFRule1);
                
                List<Rule> rulesList = controller.AService.requestPendingRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(correctPRulesList,rulesList);
            }

            [TestMethod]
            public void ApproverService_RequestApprovedRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule2);
                
                correctPRulesList.Add(cFRule2);
                
                List<Rule> rulesList = controller.AService.requestApprovedRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(correctPRulesList, rulesList);
            }

            [TestMethod]
            public void ApproverService_RequestRejectedRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                
                correctPRulesList.Add(cRule3);
                
                correctPRulesList.Add(cFRule3);
                List<Rule> rulesList = controller.AService.requestRejectedRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(correctPRulesList, rulesList);
            }

            //[TestMethod]
            //public void ApproverService_ApproveRule_CorrectApprovedRules()
            //{

            //}

            //[TestMethod]
            //public void ApproverService_RejectRule_CorrectRejectedRules()
            //{

            //}
            [TestMethod]
            public void ApproverService_ApprovedRulesNum_ReturnCorrectNumberOfApprovedRules()
            {
                int approvedRuleNum = controller.AService.approvedRulesNum(controller.CRulesList, controller.FCRulesList);
                Assert.AreEqual(approvedRuleNum, 2);
            }

            [TestMethod]
            public void ApproverService_RejectedRulesNum_ReturnCorrectNumberOfRejectedRules()
            {
                int rejectedRuleNum = controller.AService.rejectedRulesNum(controller.CRulesList, controller.FCRulesList);
                Assert.AreEqual(rejectedRuleNum, 2);
            }
            [TestMethod]
            public void ApproverService_SuccessRate_ReturnCorrectSuccessRate()
            {
                double successRate = controller.AService.successRate(controller.CRulesList, controller.FCRulesList);
                Assert.AreEqual(successRate, 0.5);
            }
            //[TestMethod]
            //public void ApproverService_CountUserApprovedRules_ReturnCorrectNumberOfApprovedRulesByUser()
            //{

            //}
            //[TestMethod]
            //public void ApproverService_CountUserRejectedRules_ReturnCorrectNumberOfRejectedRulesByUser()
            //{

            //}
            //[TestMethod]
            //public void ApproverService_CountUserPendingRules_ReturnCorrectNumberOfPendingRulesByUser()
            //{

            //}
            //[TestMethod]
            //public void ApproverService_UserSuccessRate_ReturnCorrectSuccessRateOfUser()
            //{

            //}
            //[TestMethod]
            //public void ApproverService_OverallAveSuccessRate_ReturnCorrectNumberOfOverallAverageSuccessRate()
            //{

            //}
            //-----------------------------------EditorService-----------------------------------------
            [TestMethod]
            public void EditorService_AddNewCRule_CRuleListHaveCorrectRules()
            {
                ConversationalRule cRule4 = new ConversationalRule("c004", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
                List<ConversationalRule> correctRulesList = new List<ConversationalRule>();
                List<ConversationalRule> rulesList = new List<ConversationalRule>();
                correctRulesList.Add(cRule1);
                correctRulesList.Add(cRule2);
                correctRulesList.Add(cRule3);
                correctRulesList.Add(cRule4);
                rulesList.Add(cRule1);
                rulesList.Add(cRule2);
                rulesList.Add(cRule3);
                controller.EService.AddNewCRule(cRule4, ref rulesList);
                CollectionAssert.AreEqual(correctRulesList, rulesList);
            }
            [TestMethod]
            public void EditorService_AddNewFCRule_FCRuleListHaveCorrectRules()
            {
                FixedConversationalRule cFRule4 = new FixedConversationalRule("fc002", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
                List<FixedConversationalRule> correctRulesList = new List<FixedConversationalRule>();
                List<FixedConversationalRule> rulesList = new List<FixedConversationalRule>();
                correctRulesList.Add(cFRule1);
                correctRulesList.Add(cFRule2);
                correctRulesList.Add(cFRule3);
                correctRulesList.Add(cFRule4);
                rulesList.Add(cFRule1);
                rulesList.Add(cFRule2);
                rulesList.Add(cFRule3);
                controller.EService.AddNewFCRule(cFRule4, ref rulesList);
                CollectionAssert.AreEqual(correctRulesList, rulesList);
            }
            [TestMethod]
            public void EditorService_ShowAllPendingRules_ReturnCorrectList()
            {
                List<Rule> pendingRulesList = new List<Rule>();
                List<Rule> correctRulesList = new List<Rule>();
                correctRulesList.Add(cFRule1);
                correctRulesList.Add(cRule1);
                pendingRulesList = controller.EService.ShowAllPendingRules(controller.FCRulesList, controller.CRulesList);
                CollectionAssert.AreEqual(correctRulesList, pendingRulesList);
            }
            [TestMethod]
            public void EditorService_ShowAllRejectedRules_ReturnCorrectList()
            {
                List<Rule> rejectedRulesList = new List<Rule>();
                List<Rule> correctRulesList = new List<Rule>();
                correctRulesList.Add(cFRule3);
                correctRulesList.Add(cRule3);
                rejectedRulesList = controller.EService.ShowAllRejectedRules(controller.FCRulesList, controller.CRulesList);
                CollectionAssert.AreEqual(correctRulesList, rejectedRulesList);
            }
            //[TestMethod]
            //public void EditorService_EditPendingRule_PendingRuleSuccessEdited()
            //{

            //}
            //[TestMethod]
            //public void EditorService_DeletePendingRule_CertainPendingRuleDeleted()
            //{

            //}
            [TestMethod]
            public void EditorService_ShowCurrentUserApprovedRules_ReturnCorrectList()
            {
                List<Rule> approvedRulesList = new List<Rule>();
                List<Rule> correctRulesList = new List<Rule>();
                correctRulesList.Add(cRule2);
                approvedRulesList = controller.EService.ShowCurrentUserApprovedRules(frank, controller.FCRulesList, controller.CRulesList);
                CollectionAssert.AreEqual(correctRulesList, approvedRulesList);
            }
            //[TestMethod]
            //public void EditorService_ShowCurrentUserApprovedRulesCount_ReturnCorrectNumberOfApprovedRules()
            //{

            //}
            //[TestMethod]
            //public void EditorService_ShowCurrentUserSuccessRate_ReturnCorrectSuccessRate()
            //{

            //}

        }
    }
        
}
