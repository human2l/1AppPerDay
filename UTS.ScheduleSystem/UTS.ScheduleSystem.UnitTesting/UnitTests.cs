﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using UTS.ScheduleSystem.MainLogic;
using System.Configuration;


namespace UTS.ScheduleSystem.UnitTesting
{
    [DeploymentItem("app.config")]

    [TestClass]
    public class UnitTest
    {
        private static Controller controller = new Controller();
        private static DataHandler dataHandler = new DataHandler();

        private static List<FixedConversationalRule> tempFixedConversationalRulesList = new List<FixedConversationalRule>();
        private static List<ConversationalRule> tempConversationalRulesList = new List<ConversationalRule>();
        private static List<MealSchedule> tempMealScheduleList = new List<MealSchedule>();

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
        ConversationalRule cRule1 = new ConversationalRule("c1", "When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Pending);
        ConversationalRule cRule11 = new ConversationalRule("c4", "When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Pending);
        ConversationalRule cRule2 = new ConversationalRule("c2", "When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Approved);
        ConversationalRule cRule21 = new ConversationalRule("c5", "When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Pending);
        ConversationalRule cRule3 = new ConversationalRule("c3", "When will I have meal with { topic } blah", "It's { topic } blah", "u001 u002", Status.Rejected);
        ConversationalRule cRule31 = new ConversationalRule("c6", "When will I have meal with { topic } blah", "{p1}", "u001 u002", Status.Pending);
        FixedConversationalRule cFRule1 = new FixedConversationalRule("fc1", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
        FixedConversationalRule cFRule11 = new FixedConversationalRule("fc4", "How do you do", "I'm fine, fuck you, and you?", "u001", Status.Pending);
        FixedConversationalRule cFRule2 = new FixedConversationalRule("fc2", "What is your name", "Are you flirting with me?", "u001", Status.Approved);
        FixedConversationalRule cFRule21 = new FixedConversationalRule("fc5", "What is your name", "Are you flirting with me?", "u001", Status.Pending);
        FixedConversationalRule cFRule3 = new FixedConversationalRule("fc3", "I'm not good", "So go fuck yourself", "u001", Status.Rejected);
        FixedConversationalRule cFRule31 = new FixedConversationalRule("fc6", "I'm not good", "So go fuck yourself", "u001", Status.Pending);

        ConversationalRule testCRule;
        FixedConversationalRule testFCRule;

        [ClassInitialize]
        public static void StartAllTest(TestContext testContext)
        {
            //Backup the database into memory
            tempFixedConversationalRulesList = controller.FixedConversationalRulesList;
            tempConversationalRulesList = controller.ConversationalRulesList;
            tempMealScheduleList = controller.MealScheduleList;
        }

        [TestInitialize]
        public void StartTest()
        {
            Debug.WriteLine("setup");


            // Empty the database
            Clear();
            //dataHandler.AddFixedConversationalRule(cFRule1);
            //dataHandler.AddConversationalRule(cRule1);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.FixedConversationalRulesList.Add(cFRule1);
            //controller.FixedConversationalRulesList.Add(cFRule2);
            //controller.FixedConversationalRulesList.Add(cFRule3);
        }

        [TestCleanup()]
        public void TerminateTest()
        {
            Debug.WriteLine("cleanup");
            // Empty the database
            Clear();

            
        }

        [ClassCleanup]
        public static void TerminateAllTest()
        {
            // Restore the database from backup
            foreach (FixedConversationalRule fcRule in tempFixedConversationalRulesList)
            {
                dataHandler.AddFixedConversationalRule(fcRule);
            }

            foreach (ConversationalRule cRule in tempConversationalRulesList)
            {
                dataHandler.AddConversationalRule(cRule);
            }

            foreach (MealSchedule m in tempMealScheduleList)
            {
                dataHandler.AddMealschedule(m.Id, m.Topic, m.Participants, m.Location, m.StartDate, m.EndDate, m.LastEditUserId);
            }
        }

        private void Clear()
        {
            Debug.WriteLine("clear");
            dataHandler.RemoveAllFixedConversationalRule();
            dataHandler.RemoveAllConversationalRule();
            dataHandler.RemoveAllMealschedule();
        }

        //private void Clear()
        //{
        //    Debug.WriteLine("clear");
        //    controller.ConversationalRulesList = new List<ConversationalRule>();
        //    controller.FixedConversationalRulesList = new List<FixedConversationalRule>();
        //    controller.MealScheduleList = new List<MealSchedule>();
        //}

        private Boolean compareTwoRules(Rule rule1, Rule rule2)
        {
            Boolean isSame = (rule1.Id.Equals(rule2.Id) &&
                rule1.Input.Equals(rule2.Input) &&
                rule1.Output.Equals(rule2.Output) &&
                rule1.RelatedUsersId.Equals(rule2.RelatedUsersId) &&
                rule1.Status.Equals(rule2.Status)) ? true : false;
            return isSame;
        }

        [TestMethod]
        public void ApproverService_RequestPendingRulesList_ReturenCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.FixedConversationalRulesList.Add(cFRule1);
            //controller.FixedConversationalRulesList.Add(cFRule2);
            //controller.FixedConversationalRulesList.Add(cFRule3);
            //List<Rule> correctPRulesList = new List<Rule>();
            //correctPRulesList.Add(cRule1);

            //correctPRulesList.Add(cFRule1);
            //CollectionAssert.AreEqual(correctPRulesList, rulesList);
            List<Rule> pendingList = controller.ApproverService.RequestPendingRulesList();
            foreach(Rule rule in pendingList)
            {
                Assert.AreEqual<Status>(Status.Pending, rule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestApprovedRulesList_ReturenCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.FixedConversationalRulesList.Add(cFRule1);
            //controller.FixedConversationalRulesList.Add(cFRule2);
            //controller.FixedConversationalRulesList.Add(cFRule3);
            //List<Rule> correctPRulesList = new List<Rule>();
            //correctPRulesList.Add(cRule2);

            //correctPRulesList.Add(cFRule2);

            //List<Rule> rulesList = controller.ApproverService.RequestApprovedRulesList();
            //CollectionAssert.AreEqual(correctPRulesList, rulesList);
            List<Rule> approvedList = controller.ApproverService.RequestApprovedRulesList();
            foreach (Rule rule in approvedList)
            {
                Assert.AreEqual<Status>(Status.Approved, rule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_RequestRejectedRulesList_ReturenCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            //List<Rule> correctPRulesList = new List<Rule>();

            //correctPRulesList.Add(cRule3);

            //correctPRulesList.Add(cFRule3);
            //List<Rule> rulesList = controller.ApproverService.RequestRejectedRulesList(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            //CollectionAssert.AreEqual(correctPRulesList, rulesList);
            List<Rule> rejectedList = controller.ApproverService.RequestRejectedRulesList();
            foreach (Rule rule in rejectedList)
            {
                Assert.AreEqual<Status>(Status.Rejected, rule.Status);
            }
        }

        [TestMethod]
        public void ApproverService_ApproveRule_CorrectApprovedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddFixedConversationalRule(cFRule1);
            controller.ApproverService.ApproveRule(cRule1.Id);
            controller.ApproverService.ApproveRule(cFRule1.Id);
            testCRule = dataHandler.FindConversationalRuleById(cRule1.Id);
            testFCRule = dataHandler.FindFixedConversationalRuleById(cFRule1.Id);
            Assert.AreEqual<Status>(Status.Approved, testCRule.Status);
            Assert.AreEqual<Status>(Status.Approved, testFCRule.Status);
            //List<ConversationalRule> CRulesList = new List<ConversationalRule>();
            //List<FixedConversationalRule> FCRulesList = new List<FixedConversationalRule>();
            //CRulesList.Add(cRule1);
            //FCRulesList.Add(cFRule1);
            //controller.ApproverService.ApproveRule(cRule1.Id, ref CRulesList, ref FCRulesList);
            //controller.ApproverService.ApproveRule(cFRule1.Id, ref CRulesList, ref FCRulesList);
            //Assert.AreEqual<Status>(Status.Approved, CRulesList[0].Status);
            //Assert.AreEqual<Status>(Status.Approved, FCRulesList[0].Status);

        }

        [TestMethod]
        public void ApproverService_RejectRule_CorrectRejectedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddFixedConversationalRule(cFRule1);
            controller.ApproverService.RejectRule(cRule1.Id);
            controller.ApproverService.RejectRule(cFRule1.Id);
            testCRule = dataHandler.FindConversationalRuleById(cRule1.Id);
            testFCRule = dataHandler.FindFixedConversationalRuleById(cFRule1.Id);
            Assert.AreEqual<Status>(Status.Rejected, testCRule.Status);
            Assert.AreEqual<Status>(Status.Rejected, testFCRule.Status);
            //List<ConversationalRule> CRulesList = new List<ConversationalRule>();
            //List<FixedConversationalRule> FCRulesList = new List<FixedConversationalRule>();
            //CRulesList.Add(cRule1);
            //FCRulesList.Add(cFRule1);
            //controller.ApproverService.RejectRule(cRule1.Id, ref CRulesList, ref FCRulesList);
            //controller.ApproverService.RejectRule(cFRule1.Id, ref CRulesList, ref FCRulesList);
            //Assert.AreEqual<Status>(Status.Rejected, CRulesList[0].Status);
            //Assert.AreEqual<Status>(Status.Rejected, FCRulesList[0].Status);
        }

        [TestMethod]
        public void ApproverService_ApprovedRulesNum_ReturnCorrectNumberOfApprovedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int approvedRuleNum = controller.ApproverService.ApprovedRulesNum();
            Assert.AreEqual(2, approvedRuleNum);
        }

        [TestMethod]
        public void ApproverService_RejectedRulesNum_ReturnCorrectNumberOfRejectedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int rejectedRuleNum = controller.ApproverService.RejectedRulesNum();
            Assert.AreEqual(2, rejectedRuleNum);
        }

        [TestMethod]
        public void ApproverService_SuccessRate_ReturnCorrectSuccessRate()
        {
            double successRate = controller.ApproverService.SuccessRate(controller.ConversationalRulesList, controller.FixedConversationalRulesList);
            Assert.AreEqual(0.5, successRate);
        }

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

        //-----------------------------------EditorService-----------------------------------------

        [TestMethod]
        public void EditorService_AddNewCRule_CRuleListHaveCorrectRules()
        {
            //ConversationalRule cRule4 = new ConversationalRule("c4", "What will I surpose to eat on {p1}", "{p1}", "u001 u002", Status.Pending);
            //List<ConversationalRule> correctRulesList = new List<ConversationalRule>();
            //List<ConversationalRule> rulesList = new List<ConversationalRule>();
            //correctRulesList.Add(cRule1);
            //correctRulesList.Add(cRule2);
            //correctRulesList.Add(cRule3);
            //correctRulesList.Add(cRule4);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.ConversationalRulesList.Add(cRule4);
            controller.EditorService.AddNewCRule(cRule1.Input, cRule1.Output, cRule1.RelatedUsersId);
            testCRule = dataHandler.FindConversationalRuleById(cRule1.Id);
            //controller.EditorService.AddNewCRule(cRule2.Input, cRule2.Output, cRule2.RelatedUsersId);
            //controller.EditorService.AddNewCRule(cRule3.Input, cRule3.Output, cRule3.RelatedUsersId);
            //controller.EditorService.AddNewCRule(cRule4.Input, cRule4.Output, cRule4.RelatedUsersId);
            //CollectionAssert.AreEqual(correctRulesList, controller.ConversationalRulesList);
            Assert.IsTrue(compareTwoRules(cRule1, testCRule));
        }
        [TestMethod]
        public void EditorService_AddNewFCRule_FCRuleListHaveCorrectRules()
        {
            //Clear();
            //FixedConversationalRule cFRule4 = new FixedConversationalRule("fc4", "I'm not good", "So go fuck yourself", "u001", Status.Pending);
            //List<FixedConversationalRule> correctRulesList = new List<FixedConversationalRule>();
            //List<FixedConversationalRule> rulesList = new List<FixedConversationalRule>();
            //correctRulesList.Add(cFRule1);
            //correctRulesList.Add(cFRule2);
            //correctRulesList.Add(cFRule3);
            //correctRulesList.Add(cFRule4);
            controller.EditorService.AddNewFCRule(cFRule1.Input, cFRule1.Output, cFRule1.RelatedUsersId);
            testFCRule = dataHandler.FindFixedConversationalRuleById(cFRule1.Id);
            //controller.EditorService.AddNewFCRule(cFRule2.Input, cFRule2.Output, cFRule2.RelatedUsersId);
            //controller.EditorService.AddNewFCRule(cFRule3.Input, cFRule3.Output, cFRule3.RelatedUsersId);
            //controller.EditorService.AddNewFCRule(cFRule4.Input, cFRule4.Output, cFRule4.RelatedUsersId);
            //CollectionAssert.AreEqual(correctRulesList, controller.FixedConversationalRulesList);
            Assert.IsTrue(compareTwoRules(cFRule1, testFCRule));
        }
        [TestMethod]
        public void EditorService_ShowAllPendingRules_ReturnCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            //controller.ConversationalRulesList.Add(cRule1);
            //controller.ConversationalRulesList.Add(cRule2);
            //controller.ConversationalRulesList.Add(cRule3);
            //controller.FixedConversationalRulesList.Add(cFRule1);
            //controller.FixedConversationalRulesList.Add(cFRule2);
            //controller.FixedConversationalRulesList.Add(cFRule3);
            //List<Rule> correctPRulesList = new List<Rule>();
            //correctPRulesList.Add(cRule1);

            //correctPRulesList.Add(cFRule1);
            //CollectionAssert.AreEqual(correctPRulesList, rulesList);
            List<Rule> pendingList = controller.EditorService.ShowAllPendingRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            foreach (Rule rule in pendingList)
            {
                Assert.AreEqual<Status>(Status.Pending, rule.Status);
            }
        }
        [TestMethod]
        public void EditorService_ShowAllRejectedRules_ReturnCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            List<Rule> rejectedList = controller.EditorService.ShowAllRejectedRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            foreach (Rule rule in rejectedList)
            {
                Assert.AreEqual<Status>(Status.Rejected, rule.Status);
            }
            //List<Rule> rejectedRulesList = new List<Rule>();
            //List<Rule> correctRulesList = new List<Rule>();
            //correctRulesList.Add(cFRule3);
            //correctRulesList.Add(cRule3);
            //rejectedRulesList = controller.EditorService.ShowAllRejectedRules(controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            //CollectionAssert.AreEqual(correctRulesList, rejectedRulesList);
        }
        //[TestMethod]
        //public void EditorService_EditPendingRule_PendingRuleSuccessEdited()
        //{

        //}
        //[TestMethod]
        //public void EditorService_DeletePendingRule_CertainPendingRuleDeleted()
        //{

        //}
        //[TestMethod]
        //public void EditorService_ShowCurrentUserApprovedRules_ReturnCorrectList()
        //{
        //    List<Rule> approvedRulesList = new List<Rule>();
        //    List<Rule> correctRulesList = new List<Rule>();
        //    correctRulesList.Add(cRule2);
        //    approvedRulesList = controller.EditorService.ShowCurrentUserApprovedRules(frank, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
        //    CollectionAssert.AreEqual(correctRulesList, approvedRulesList);
        //}
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


    //[TestClass]

    //public class UnitTests
    //{
    //public class TestController
    //{
    //    private User currentUser;
    //    private List<User> userList = new List<User>();
    //    private List<ConversationalRule> conversationalRulesList = new List<ConversationalRule>();
    //    private List<FixedConversationalRule> fixedConversationalRulesList = new List<FixedConversationalRule>();
    //    private List<MealSchedule> mealScheduleList = new List<MealSchedule>();
    //    private ConversationService conversationService = new ConversationService();
    //    private DataMaintainerService dataMaintainerService = new DataMaintainerService();
    //    private EditorService editorService = new EditorService();
    //    private ApproverService approverService = new ApproverService();

    //    public TestController() { }

    //    public User CurrentUser { get => currentUser; set => currentUser = value; }
    //    public List<User> UserList { get => userList; set => userList = value; }
    //    public List<ConversationalRule> ConversationalRulesList { get => conversationalRulesList; set => conversationalRulesList = value; }
    //    public List<FixedConversationalRule> FixedConversationalRulesList { get => fixedConversationalRulesList; set => fixedConversationalRulesList = value; }
    //    public List<MealSchedule> MealScheduleList { get => mealScheduleList; set => mealScheduleList = value; }
    //    public ConversationService ConversationService { get => conversationService; set => conversationService = value; }
    //    public DataMaintainerService DataMaintainerService { get => dataMaintainerService; set => dataMaintainerService = value; }
    //    public EditorService EditorService { get => editorService; set => editorService = value; }
    //    public ApproverService ApproverService { get => approverService; set => approverService = value; }
    //}
