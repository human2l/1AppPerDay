using System;
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

        User frank = new User("u001", "FRANK", "wtf", "FRANK@wtf.com", Role.DMnEnA);
        User frank2 = new User("u002", "FRANK2", "222", "FRANK@2.com", Role.DMnEnA);
        User frank3 = new User("u003", "FRANK2333", "2333", "FRANK@2333.com", Role.DMnEnA);
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
        MealSchedule mS1 = new MealSchedule("ms1", "a", "b", "c", "d", "e", "u001");
        MealSchedule mS2 = new MealSchedule("ms2", "f", "g", "h", "i", "j", "u001");
        MealSchedule mS3 = new MealSchedule("ms3", "k", "l", "m", "n", "o", "u001");

        ConversationalRule testCRule;
        FixedConversationalRule testFCRule;
        MealSchedule testMSchedule;

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
                dataHandler.AddMealschedule(m);
            }
        }

        private void Clear()
        {
            Debug.WriteLine("clear");
            dataHandler.RemoveAllFixedConversationalRule();
            dataHandler.RemoveAllConversationalRule();
            dataHandler.RemoveAllMealschedule();
        }

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
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddConversationalRule(cRule11);
            dataHandler.AddConversationalRule(cRule21);
            dataHandler.AddConversationalRule(cRule31);
            double successRate = controller.ApproverService.SuccessRate();
            Assert.AreEqual(0.5, successRate);
        }

        [TestMethod]
        public void ApproverService_UserRelatedApprovedRulesNum_ReturnCorrectNumberOfApprovedRulesByUser()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int UserRelatedApprovedRulesNum = controller.ApproverService.UserRelatedApprovedRulesNum(frank.Id);
            Assert.AreEqual(2, UserRelatedApprovedRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserRelatedRejectedRulesNum_ReturnCorrectNumberOfRejectedRulesByUser()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int UserRelatedRejectedRulesNum = controller.ApproverService.UserRelatedRejectedRulesNum(frank.Id);
            Assert.AreEqual(2, UserRelatedRejectedRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserRelatedPendingRulesNum_ReturnCorrectNumberOfPendingRulesByUser()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int UserRelatedPendingRulesNum = controller.ApproverService.UserRelatedPendingRulesNum(frank.Id);
            Assert.AreEqual(2, UserRelatedPendingRulesNum);
        }

        [TestMethod]
        public void ApproverService_UserSuccessRate_ReturnCorrectSuccessRateOfUser()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            double userSuccessRate = controller.ApproverService.UserSuccessRate(frank2.Id);
            Assert.AreEqual(0.5, userSuccessRate);
        }

        [TestMethod]
        public void ApproverService_OverallAveSuccessRate_ReturnCorrectNumberOfOverallAverageSuccessRate()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            double overallAveSuccessRate = controller.ApproverService.OverallAveSuccessRate();
            Assert.AreEqual(0.125, overallAveSuccessRate);
        }

        //-----------------------------------EditorService-----------------------------------------

        [TestMethod]
        public void EditorService_AddNewCRule_CRuleListHaveCorrectRules()
        {
            controller.EditorService.AddNewCRule(cRule1.Input, cRule1.Output, cRule1.RelatedUsersId);
            testCRule = dataHandler.FindConversationalRuleById(cRule1.Id);
            Assert.IsTrue(compareTwoRules(cRule1, testCRule));
        }
        [TestMethod]
        public void EditorService_AddNewFCRule_FCRuleListHaveCorrectRules()
        {
            controller.EditorService.AddNewFCRule(cFRule1.Input, cFRule1.Output, cFRule1.RelatedUsersId);
            testFCRule = dataHandler.FindFixedConversationalRuleById(cFRule1.Id);
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
        }
        [TestMethod]
        public void EditorService_EditPendingRule_PendingRuleSuccessEdited()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddFixedConversationalRule(cFRule1);
            string correctInput = "changedInput";
            string correctOutput = "changedOutput";
            controller.EditorService.EditPendingRule(frank.Id, cRule1.Id, correctInput, correctOutput, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            controller.EditorService.EditPendingRule(frank.Id, cFRule1.Id, correctInput, correctOutput, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            testCRule = dataHandler.FindConversationalRuleById("c1");
            testFCRule = dataHandler.FindFixedConversationalRuleById("fc1");
            Assert.AreEqual(correctInput, testCRule.Input);
            Assert.AreEqual(correctInput, testFCRule.Input);
            Assert.AreEqual(correctOutput, testCRule.Output);
            Assert.AreEqual(correctOutput, testFCRule.Output);
        }
        [TestMethod]
        public void EditorService_DeletePendingRule_CertainPendingRuleDeleted()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            controller.EditorService.DeletePendingRule(cRule1.Id);
            controller.EditorService.DeletePendingRule(cFRule1.Id);
            int conversationalRuleNum = controller.ConversationalRulesList.Count;
            int fixedConversationalRuleNum = controller.FixedConversationalRulesList.Count;
            Assert.AreEqual(2, conversationalRuleNum);
            Assert.AreEqual(2, fixedConversationalRuleNum);
        }
        [TestMethod]
        public void EditorService_ShowCurrentUserApprovedRules_ReturnCorrectList()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            List<Rule> approvedRulesList = new List<Rule>();
            approvedRulesList = controller.EditorService.ShowCurrentUserApprovedRules(frank, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            Assert.IsTrue(compareTwoRules(cRule2, approvedRulesList[1]));
            Assert.IsTrue(compareTwoRules(cFRule2, approvedRulesList[0]));
        }
        [TestMethod]
        public void EditorService_ShowCurrentUserApprovedRulesCount_ReturnCorrectNumberOfApprovedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int approvedRuleNum = controller.EditorService.ShowCurrentUserApprovedRulesCount(frank2, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            Assert.AreEqual(1, approvedRuleNum);
        }
        [TestMethod]
        public void EditorService_ShowCurrentUserRejectedRulesCount_ReturnCorrectNumberOfApprovedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1); 
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int rejectedRuleNum = controller.EditorService.ShowCurrentUserRejectedRulesCount(frank2, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            Assert.AreEqual(1, rejectedRuleNum);
        }
        [TestMethod]
        public void EditorService_ShowCurrentUserPendingRulesCount_ReturnCorrectNumberOfApprovedRules()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            int pendingRuleNum = controller.EditorService.ShowCurrentUserPendingRulesCount(frank2, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            Assert.AreEqual(1, pendingRuleNum);
        }
        [TestMethod]
        public void EditorService_ShowCurrentUserSuccessRate_ReturnCorrectSuccessRate()
        {
            dataHandler.AddConversationalRule(cRule1);
            dataHandler.AddConversationalRule(cRule2);
            dataHandler.AddConversationalRule(cRule3);
            dataHandler.AddFixedConversationalRule(cFRule1);
            dataHandler.AddFixedConversationalRule(cFRule2);
            dataHandler.AddFixedConversationalRule(cFRule3);
            double userSuccessRate = controller.EditorService.ShowCurrentUserSuccessRate(frank2, controller.FixedConversationalRulesList, controller.ConversationalRulesList);
            Assert.AreEqual(0.33, userSuccessRate);
        }

        //-----------------------------------DataMaintainerService-----------------------------------------

        private Boolean CompareMealSchedule(MealSchedule mealSchedule1, MealSchedule mealSchedule2)
        {
            Boolean isSame = (mealSchedule1.Id.Equals(mealSchedule2.Id)) &&
                (mealSchedule1.Topic.Equals(mealSchedule2.Topic)) &&
                (mealSchedule1.Participants.Equals(mealSchedule2.Participants)) &&
                (mealSchedule1.Location.Equals(mealSchedule2.Location)) &&
                (mealSchedule1.StartDate.Equals(mealSchedule2.StartDate)) &&
                (mealSchedule1.EndDate.Equals(mealSchedule2.EndDate)) &&
                (mealSchedule1.LastEditUserId.Equals(mealSchedule2.LastEditUserId)) ? true : false;
            return isSame;
        }

        [TestMethod]
        public void DataMaintainerService_AddMealSchedule_AddANewMealScheduleDataIntoDatabase()
        {
            controller.DataMaintainerService.AddMealSchedule(mS1.Topic, mS1.Participants, mS1.Location, mS1.StartDate, mS1.EndDate, mS1.LastEditUserId);
            testMSchedule = dataHandler.FindMealScheduleById(mS1.Id);
            Assert.IsTrue(CompareMealSchedule(mS1, testMSchedule));
        }

        [TestMethod]
        public void DataMaintainerService_DeleteMealSchedule_DeleteMealScheduleDataFromDatabase()
        {
            dataHandler.AddMealschedule(mS1);
            dataHandler.AddMealschedule(mS2);
            dataHandler.AddMealschedule(mS3);
            controller.DataMaintainerService.DeleteMealSchedule(mS1.Id);
            Assert.AreEqual(2, controller.MealScheduleList.Count);
        }

        [TestMethod]
        public void DataMaintainerService_EditMealSchedule_EditMealScheduleInDatabase()
        {
            dataHandler.AddMealschedule(mS1);
            controller.DataMaintainerService.EditMealSchedule(mS1.Id, mS2.Topic, mS2.Participants, mS2.Location, mS2.StartDate, mS2.EndDate, mS2.LastEditUserId);
            testMSchedule = dataHandler.FindMealScheduleById(mS1.Id);
            Assert.AreEqual(mS2.Topic, testMSchedule.Topic);
            Assert.AreEqual(mS2.Participants, testMSchedule.Participants);
            Assert.AreEqual(mS2.Location, testMSchedule.Location);
            Assert.AreEqual(mS2.StartDate, testMSchedule.StartDate);
            Assert.AreEqual(mS2.EndDate, testMSchedule.EndDate);
        }
    }
}
