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
            dataHandler.AddMealschedule(ms.Id, ms.Topic, ms.Participants, ms.Location, ms.StartDate, ms.EndDate, ms.LastEditUserId);
            //mealScheduleList.Add(mealSchedule);

        }


        // Delete a mealschedule from database due to id
        public void DeleteMealSchedule(string id)
        {
            //for datamaintainer to delete meal from mealschedule list
            dataHandler.DeleteMealschedule(id);
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string laseEditor)
        {
            //System.Diagnostics.Debug.WriteLine(ms);
            dataHandler.ChangeOnMealschedule(id, topic, participants, location, startDate, endDate, laseEditor);
        }
    }
}
