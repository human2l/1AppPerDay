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
            dataMaintainerService.AddMealSchedule(UnitTestPublic.ms1);
            MealSchedule testMSchedule = MealScheduleHandler.FindMealScheduleById("1");
            Assert.IsTrue(UnitTestPublic.CompareMealSchedule(UnitTestPublic.ms1, testMSchedule));
        }

        [TestMethod]
        public void DataMaintainerService_DeleteMealSchedule_DeleteMealScheduleDataFromDatabase()
        {
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms2);
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms3);
            dataMaintainerService.DeleteMealScheduleById("1");
            Assert.AreEqual(2, MealScheduleHandler.FindAllMealSchedules().Count);
        }

        [TestMethod]
        public void DataMaintainerService_EditMealSchedule_EditMealScheduleInDatabase()
        {
            MealScheduleHandler.AddMealschedule(UnitTestPublic.ms1);

            MealSchedule newMealSchedule = UnitTestPublic.ms2;
            newMealSchedule.Id = 1;

            dataMaintainerService.EditMealSchedule(newMealSchedule);
            MealSchedule testMSchedule = MealScheduleHandler.FindMealScheduleById("1");
            Assert.IsTrue(UnitTestPublic.CompareMealSchedule(UnitTestPublic.ms2, testMSchedule));
        }
    }
}
