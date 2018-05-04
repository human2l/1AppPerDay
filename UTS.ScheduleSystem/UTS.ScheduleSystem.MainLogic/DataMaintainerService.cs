using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class DataMaintainerService
    {
        private DataHandler dataHandler;
        //private DatabaseAdapter databaseAdapter = new DatabaseAdapter();

        public DataMaintainerService()
        {
            dataHandler = new DataHandler();
        }

        // Add a new mealschedule to database
        public void AddMealSchedule(string topic, string participants, string location, string startDate, string endDate, string lastEditorUserId)
        {

            //for datamaintainer to add meal

            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", dataHandler.FindLastMealscheduleId()), topic, participants, location, startDate, endDate, lastEditorUserId);
            dataHandler.AddMealschedule(ms);

        }


        // Delete a mealschedule from database due to id
        public void DeleteMealSchedule(string id)
        {
            dataHandler.DeleteMealschedule(id);
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string laseEditor)
        {
            dataHandler.ChangeOnMealschedule(id, topic, participants, location, startDate, endDate, laseEditor);
        }

        public MealSchedule FindMealScheduleById (string id)
        {
            return dataHandler.FindMealScheduleById(id);
        }
    }
}
