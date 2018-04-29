using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using UTS.ScheduleSystem.MainLogic;

namespace UTS.ScheduleSystem.UnitTesting
{
    [TestClass]
    public class UnitTests
    {
        public class TestController
        {
            private User currentUser;
            private List<User> userList = new List<User>();
            private List<ConversationalRule> conversationalRulesList = new List<ConversationalRule>();
            private List<FixedConversationalRule> fixedConversationalRulesList = new List<FixedConversationalRule>();
            private List<MealSchedule> mealScheduleList = new List<MealSchedule>();
            private ConversationService conversationService = new ConversationService();
            private DataMaintainerService dataMaintainerService = new DataMaintainerService();
            private EditorService editorService = new EditorService();
            private ApproverService approverService = new ApproverService();

            public TestController() { }

            public User CurrentUser { get => currentUser; set => currentUser = value; }
            public List<User> UserList { get => userList; set => userList = value; }
            public List<ConversationalRule> ConversationalRulesList { get => conversationalRulesList; set => conversationalRulesList = value; }
            public List<FixedConversationalRule> FixedConversationalRulesList { get => fixedConversationalRulesList; set => fixedConversationalRulesList = value; }
            public List<MealSchedule> MealScheduleList { get => mealScheduleList; set => mealScheduleList = value; }
            public ConversationService ConversationService { get => conversationService; set => conversationService = value; }
            public DataMaintainerService DataMaintainerService { get => dataMaintainerService; set => dataMaintainerService = value; }
            public EditorService EditorService { get => editorService; set => editorService = value; }
            public ApproverService ApproverService { get => approverService; set => approverService = value; }
        }
        [TestClass]
        public class UnitTest
        {
            TestController controller = new TestController();
            User frank = new User("u1", "FRANK", "wtf", "FRANK@wtf.com", Role.DMnEnA);
            User frank2 = new User("u2", "FRANK2", "222", "FRANK@2.com", Role.DMnEnA);
            User frank3 = new User("u3", "FRANK2333", "2333", "FRANK@2333.com", Role.DMnEnA);
            User DM = new User("uDM", "DM", "DM", "DM@DM.com", Role.DM);
            User E = new User("uE", "E", "E", "E@E.com", Role.E);
            User A = new User("uA", "A", "A", "A@A.com", Role.A);
            User DMnE = new User("uDMnE", "DMnE", "DMnE", "DMnE@DMnE.com", Role.DMnE);
            User DMnA = new User("uDMnA", "DMnA", "DMnA", "DMnA@DMnA.com", Role.DMnA);
            User EnA = new User("uEnA", "EnA", "EnA", "EnA@EnA.com", Role.EnA);
            User DMnEnA = new User("uDMnEnA", "DMnEnA", "DMnEnA", "DMnEnA@DMnEnA.com", Role.DMnEnA);
            User None = new User("uNone", "None", "None", "None@None.com", Role.None);
            ConversationalRule cRule1 = new ConversationalRule("c1", "When will I have meal with {p1}", "It's {p1}", "u001 u002", Status.Pending);
            ConversationalRule cRule11 = new ConversationalRule("c11", "When will I have meal with {p1}", "It's {p1}", "u001 u002", Status.Pending);
            ConversationalRule cRule2 = new ConversationalRule("c2", "Who will I have meal with on {p1}", "It's {p1}", "u001 u002", Status.Approved);
            ConversationalRule cRule21 = new ConversationalRule("c21", "Who will I have meal with on {p1}", "It's {p1}", "u001 u002", Status.Approved);
            ConversationalRule cRule3 = new ConversationalRule("c3", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            ConversationalRule cRule31 = new ConversationalRule("c31", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            FixedConversationalRule cFRule1 = new FixedConversationalRule("fc1", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            FixedConversationalRule cFRule11 = new FixedConversationalRule("fc11", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
            FixedConversationalRule cFRule2 = new FixedConversationalRule("fc2", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
            FixedConversationalRule cFRule21 = new FixedConversationalRule("fc21", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
            FixedConversationalRule cFRule3 = new FixedConversationalRule("fc3", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            FixedConversationalRule cFRule31 = new FixedConversationalRule("fc31", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            [TestInitialize]
            public void Setup()
            {

                //controller.ConversationalRulesList.Add(cRule1);
                //controller.ConversationalRulesList.Add(cRule2);
                //controller.ConversationalRulesList.Add(cRule3);
                //controller.FixedConversationalRulesList.Add(cFRule1);
                //controller.FixedConversationalRulesList.Add(cFRule2);
                //controller.FixedConversationalRulesList.Add(cFRule3);
            }

            private void clear()
            {
                controller.ConversationalRulesList = new List<ConversationalRule>();
                controller.FixedConversationalRulesList = new List<FixedConversationalRule>();
                controller.MealScheduleList = new List<MealSchedule>();
            }

            [TestMethod]

            public void ApproverService_RequestPendingRulesList_ReturenCorrectList()
            {
                
                controller.ConversationalRulesList.Add(cRule1);
                controller.ConversationalRulesList.Add(cRule2);
                controller.ConversationalRulesList.Add(cRule3);
                controller.FixedConversationalRulesList.Add(cFRule1);
                controller.FixedConversationalRulesList.Add(cFRule2);
                controller.FixedConversationalRulesList.Add(cFRule3);
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule1);
                
                correctPRulesList.Add(cFRule1);
                
                List<Rule> rulesList = controller.ApproverService.RequestPendingRulesList();
                CollectionAssert.AreEqual(correctPRulesList,rulesList);
                //clear();
            }

            [TestMethod]
            public void ApproverService_RequestApprovedRulesList_ReturenCorrectList()
            {
                controller.ConversationalRulesList.Add(cRule1);
                controller.ConversationalRulesList.Add(cRule2);
                controller.ConversationalRulesList.Add(cRule3);
                controller.FixedConversationalRulesList.Add(cFRule1);
                controller.FixedConversationalRulesList.Add(cFRule2);
                controller.FixedConversationalRulesList.Add(cFRule3);
                List<Rule> correctPRulesList = new List<Rule>();
                correctPRulesList.Add(cRule2);
                
                correctPRulesList.Add(cFRule2);
                
                List<Rule> rulesList = controller.ApproverService.RequestApprovedRulesList();
                CollectionAssert.AreEqual(correctPRulesList, rulesList);
                clear();
            }

            //[TestMethod]
            //public void ApproverService_RequestRejectedRulesList_ReturenCorrectList()
            //{
            //    List<Rule> correctPRulesList = new List<Rule>();
                
            //    correctPRulesList.Add(cRule3);
                
            //    correctPRulesList.Add(cFRule3);
            //    List<Rule> rulesList = controller.ApproverService.RequestRejectedRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    CollectionAssert.AreEqual(correctPRulesList, rulesList);
            //}

            ////[TestMethod]
            ////public void ApproverService_ApproveRule_CorrectApprovedRules()
            ////{
            ////    List<ConversationalRule> CRulesList = new List<ConversationalRule>();
            ////    List<FixedConversationalRule> FCRulesList = new List<FixedConversationalRule>();
            ////    CRulesList.Add(cRule1);
            ////    FCRulesList.Add(cFRule1);
            ////    controller.ApproverService.ApproveRule(cRule1.Id, ref CRulesList, ref FCRulesList);
            ////    controller.ApproverService.ApproveRule(cFRule1.Id, ref CRulesList, ref FCRulesList);
            ////    Assert.AreEqual<Status>(Status.Approved, CRulesList[0].Status);
            ////    Assert.AreEqual<Status>(Status.Approved, FCRulesList[0].Status);

            ////}

            ////[TestMethod]
            ////public void ApproverService_RejectRule_CorrectRejectedRules()
            ////{
            ////    List<ConversationalRule> CRulesList = new List<ConversationalRule>();
            ////    List<FixedConversationalRule> FCRulesList = new List<FixedConversationalRule>();
            ////    CRulesList.Add(cRule1);
            ////    FCRulesList.Add(cFRule1);
            ////    controller.ApproverService.RejectRule(cRule1.Id, ref CRulesList, ref FCRulesList);
            ////    controller.ApproverService.RejectRule(cFRule1.Id, ref CRulesList, ref FCRulesList);
            ////    Assert.AreEqual<Status>(Status.Rejected, CRulesList[0].Status);
            ////    Assert.AreEqual<Status>(Status.Rejected, FCRulesList[0].Status);
            ////}

            //[TestMethod]
            //public void ApproverService_ApprovedRulesNum_ReturnCorrectNumberOfApprovedRules()
            //{
            //    int approvedRuleNum = controller.ApproverService.ApprovedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(2, approvedRuleNum);
            //}

            //[TestMethod]
            //public void ApproverService_RejectedRulesNum_ReturnCorrectNumberOfRejectedRules()
            //{
            //    int rejectedRuleNum = controller.ApproverService.RejectedRulesNum(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(2, rejectedRuleNum);
            //}

            //[TestMethod]
            //public void ApproverService_SuccessRate_ReturnCorrectSuccessRate()
            //{
            //    double successRate = controller.ApproverService.SuccessRate(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(0.5, successRate);
            //}

            //[TestMethod]
            //public void ApproverService_UserRelatedApprovedRulesNum_ReturnCorrectNumberOfApprovedRulesByUser()
            //{
            //    int UserRelatedApprovedRulesNum = controller.ApproverService.UserRelatedApprovedRulesNum(frank, controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(1, UserRelatedApprovedRulesNum);
            //}

            //[TestMethod]
            //public void ApproverService_UserRelatedRejectedRulesNum_ReturnCorrectNumberOfRejectedRulesByUser()
            //{
            //    int UserRelatedRejectedRulesNum = controller.ApproverService.UserRelatedRejectedRulesNum(frank, controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(1, UserRelatedRejectedRulesNum);
            //}

            //[TestMethod]
            //public void ApproverService_UserRelatedPendingRulesNum_ReturnCorrectNumberOfPendingRulesByUser()
            //{
            //    int UserRelatedPendingRulesNum = controller.ApproverService.UserRelatedRejectedRulesNum(frank, controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(1, UserRelatedPendingRulesNum);
            //}

            //[TestMethod]
            //public void ApproverService_UserSuccessRate_ReturnCorrectSuccessRateOfUser()
            //{
            //    double UserSuccessRate = controller.ApproverService.UserSuccessRate(frank, controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Assert.AreEqual(0.5, UserSuccessRate);
            //}

            //[TestMethod]
            //public void ApproverService_OverallAveSuccessRate_ReturnCorrectNumberOfOverallAverageSuccessRate()
            //{
            //    List<User> users = new List<User>();
            //    users.Add(frank2);
            //    users.Add(frank);
            //    double OverallAveSuccessRate = controller.ApproverService.OverallAveSuccessRate(users, controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //    Debug.WriteLine(controller.ApproverService.UserRelatedApprovedRulesNum(frank2, controller.ConversationalRulesList, controller.FixedConversationalRulesList));
            //    Debug.WriteLine(controller.ApproverService.UserRelatedRejectedRulesNum(frank2, controller.ConversationalRulesList, controller.FixedConversationalRulesList));
            //    Debug.WriteLine(controller.ApproverService.UserSuccessRate(frank2, controller.ConversationalRulesList, controller.FixedConversationalRulesList));
            //    Assert.AreEqual(0.25, OverallAveSuccessRate);
            //}

            ////-----------------------------------EditorService-----------------------------------------
            //[TestMethod]
            //public void EditorService_AddNewCRule_CRuleListHaveCorrectRules()
            //{
            //    ConversationalRule cRule4 = new ConversationalRule("c004", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Rejected);
            //    List<ConversationalRule> correctRulesList = new List<ConversationalRule>();
            //    List<ConversationalRule> rulesList = new List<ConversationalRule>();
            //    correctRulesList.Add(cRule1);
            //    correctRulesList.Add(cRule2);
            //    correctRulesList.Add(cRule3);
            //    correctRulesList.Add(cRule4);
            //    rulesList.Add(cRule1);
            //    rulesList.Add(cRule2);
            //    rulesList.Add(cRule3);
            //    //controller.EditorService.AddNewCRule(cRule4, ref rulesList);
            //    CollectionAssert.AreEqual(correctRulesList, rulesList);
            //}
            //[TestMethod]
            //public void EditorService_AddNewFCRule_FCRuleListHaveCorrectRules()
            //{
            //    FixedConversationalRule cFRule4 = new FixedConversationalRule("fc002", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
            //    List<FixedConversationalRule> correctRulesList = new List<FixedConversationalRule>();
            //    List<FixedConversationalRule> rulesList = new List<FixedConversationalRule>();
            //    correctRulesList.Add(cFRule1);
            //    correctRulesList.Add(cFRule2);
            //    correctRulesList.Add(cFRule3);
            //    correctRulesList.Add(cFRule4);
            //    rulesList.Add(cFRule1);
            //    rulesList.Add(cFRule2);
            //    rulesList.Add(cFRule3);
            //    //controller.EditorService.AddNewFCRule(cFRule4, ref rulesList);
            //    CollectionAssert.AreEqual(correctRulesList, rulesList);
            //}
            //[TestMethod]
            //public void EditorService_ShowAllPendingRules_ReturnCorrectList()
            //{
            //    List<Rule> pendingRulesList = new List<Rule>();
            //    List<Rule> correctRulesList = new List<Rule>();
            //    correctRulesList.Add(cFRule1);
            //    correctRulesList.Add(cRule1);
            //    pendingRulesList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            //    CollectionAssert.AreEqual(correctRulesList, pendingRulesList);
            //}
            //[TestMethod]
            //public void EditorService_ShowAllRejectedRules_ReturnCorrectList()
            //{
            //    List<Rule> rejectedRulesList = new List<Rule>();
            //    List<Rule> correctRulesList = new List<Rule>();
            //    correctRulesList.Add(cFRule3);
            //    correctRulesList.Add(cRule3);
            //    rejectedRulesList = controller.EditorService.ShowAllRejectedRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            //    CollectionAssert.AreEqual(correctRulesList, rejectedRulesList);
            //}
            ////[TestMethod]
            ////public void EditorService_EditPendingRule_PendingRuleSuccessEdited()
            ////{

            ////}
            ////[TestMethod]
            ////public void EditorService_DeletePendingRule_CertainPendingRuleDeleted()
            ////{

            ////}
            //[TestMethod]
            //public void EditorService_ShowCurrentUserApprovedRules_ReturnCorrectList()
            //{
            //    List<Rule> approvedRulesList = new List<Rule>();
            //    List<Rule> correctRulesList = new List<Rule>();
            //    correctRulesList.Add(cRule2);
            //    approvedRulesList = controller.EditorService.ShowCurrentUserApprovedRules(frank, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            //    CollectionAssert.AreEqual(correctRulesList, approvedRulesList);
            //}
            ////[TestMethod]
            ////public void EditorService_ShowCurrentUserApprovedRulesCount_ReturnCorrectNumberOfApprovedRules()
            ////{

            ////}
            ////[TestMethod]
            ////public void EditorService_ShowCurrentUserSuccessRate_ReturnCorrectSuccessRate()
            ////{

            ////}

        }
    }
        
}
