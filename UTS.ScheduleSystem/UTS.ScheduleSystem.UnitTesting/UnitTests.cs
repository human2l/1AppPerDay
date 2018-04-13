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
            [TestMethod]
            public void ControllerLists_OnInitialization_NotNull()
            {
                //print controller
            }

            [TestMethod]

            public void ApproverService_RequestPendingRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule1);
                
                correctPRulesList.Add(cFRule1);
                
                List<Rule> pRulesList = controller.AService.requestPendingRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(pRulesList,correctPRulesList);
            }

            [TestMethod]
            public void ApproverService_RequestApprovedRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule2);
                
                correctPRulesList.Add(cFRule2);
                
                List<Rule> apvRulesList = controller.AService.requestApprovedRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(apvRulesList, correctPRulesList);
            }

            [TestMethod]
            public void ApproverService_RequestRejectedRulesList_ReturenCorrectList()
            {
                List<Rule> correctPRulesList = new List<Rule>();
                
                correctPRulesList.Add(cRule3);
                
                correctPRulesList.Add(cFRule3);
                List<Rule> rjRulesList = controller.AService.requestRejectedRulesList(controller.CRulesList, controller.FCRulesList);
                CollectionAssert.AreEqual(rjRulesList, correctPRulesList);
            }

            [TestMethod]
            public void ApproverService_ApproveRule_CorrectApprovedRules()
            {

            }

            [TestMethod]
            public void ApproverService_RejectRule_CorrectRejectedRules()
            {

            }
            [TestMethod]
            public void ApproverService_ApprovedRulesNum_ReturnCorrectNumberOfApprovedRules()
            {

            }

            [TestMethod]
            public void ApproverService_RejectedRulesNum_ReturnCorrectNumberOfRejectedRules()
            {

            }
            [TestMethod]
            public void ApproverService_SuccessRate_ReturnCorrectSuccessRate()
            {

            }
            [TestMethod]
            public void ApproverService_CountUserApprovedRules_ReturnCorrectNumberOfApprovedRulesByUser()
            {

            }
            [TestMethod]
            public void ApproverService_CountUserRejectedRules_ReturnCorrectNumberOfRejectedRulesByUser()
            {

            }
            [TestMethod]
            public void ApproverService_CountUserPendingRules_ReturnCorrectNumberOfPendingRulesByUser()
            {

            }
            [TestMethod]
            public void ApproverService_UserSuccessRate_ReturnCorrectSuccessRateOfUser()
            {

            }
            [TestMethod]
            public void ApproverService_OverallAveSuccessRate_ReturnCorrectNumberOfOverallAverageSuccessRate()
            {

            }
            //-----------------------------------EditorService-----------------------------------------
            [TestMethod]
            public void EditorService_AddNewCRule_CRuleListHaveCorrectRules()
            {

            }
            [TestMethod]
            public void EditorService_AddNewFCRule_FCRuleListHaveCorrectRules()
            {

            }
            [TestMethod]
            public void EditorService_ShowAllPendingRules_ReturnCorrectList()
            {

            }
            [TestMethod]
            public void EditorService_ShowAllRejectedRules_ReturnCorrectList()
            {

            }
            [TestMethod]
            public void EditorService_EditPendingRule_PendingRuleSuccessEdited()
            {

            }
            [TestMethod]
            public void EditorService_DeletePendingRule_CertainPendingRuleDeleted()
            {

            }
            [TestMethod]
            public void EditorService_ShowCurrentUserApprovedRules_ReturnCorrectList()
            {

            }
            [TestMethod]
            public void EditorService_ShowCurrentUserApprovedRulesCount_ReturnCorrectNumberOfApprovedRules()
            {

            }
            [TestMethod]
            public void EditorService_ShowCurrentUserSuccessRate_ReturnCorrectSuccessRate()
            {

            }
            
        }
    }
        
}
