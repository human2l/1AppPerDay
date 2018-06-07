using System;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTS.ScheduleSystem.DomainLogic.UnitTests
{
    [TestClass]
    public class UnitTestDataMaintainer
    {
        private DataMaintainerService dataMaintainerService = new DataMaintainerService();


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
        public void DataMaintainerService_AddMealSchedule_SaveMealScheduleIntoDatabase()
        {
            dataMaintainerService.AddMealSchedule(
                UnitTestPublic.ms1.Topic,
                UnitTestPublic.ms1.Participants,
                UnitTestPublic.ms1.Location, 
                UnitTestPublic.ms1.StartDate,
                UnitTestPublic.ms1.EndDate,
                UnitTestPublic.ms1.LastEditUserId);
            MealSchedule testMSchedule = MealScheduleHandler.FindMealScheduleById("1");
            Assert.IsTrue(UnitTestPublic.CompareMealSchedule(UnitTestPublic.ms1, testMSchedule));
        }

        [TestMethod]
        public void DataMaintainerService_DeleteMealSchedule_DeleteMealScheduleDataFromDatabase()
        {
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms2);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms3);
            dataMaintainerService.DeleteMealSchedule("1");
            Assert.AreEqual(2, MealScheduleHandler.FindAllMealSchedules().Count);
        }

        [TestMethod]
        public void DataMaintainerService_EditMealSchedule_EditMealScheduleInDatabase()
        {
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);
            dataMaintainerService.EditMealSchedule("1",
                UnitTestPublic.ms2.Topic, 
                UnitTestPublic.ms2.Participants, 
                UnitTestPublic.ms2.Location, 
                UnitTestPublic.ms2.StartDate, 
                UnitTestPublic.ms2.EndDate, 
                UnitTestPublic.ms2.LastEditUserId);
            MealSchedule testMSchedule = MealScheduleHandler.FindMealScheduleById("1");
            Assert.IsTrue(UnitTestPublic.CompareMealSchedule(UnitTestPublic.ms2, testMSchedule));
        }
    }
}
