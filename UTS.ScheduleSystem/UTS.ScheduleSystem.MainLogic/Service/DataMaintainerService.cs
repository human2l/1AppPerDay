using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

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

            //MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", dataHandler.FindLastMealscheduleId()), topic, participants, location, startDate, endDate, lastEditorUserId);
            MealSchedule mealSchedule = new MealSchedule();
            //after change id to int: comment below
            //mealSchedule.Id = Utils.CreateIdByType("MealSchedule", MealScheduleHandler.FindLastMealscheduleId());
            mealSchedule.Topic = topic;
            mealSchedule.Participants = participants;
            mealSchedule.Location = location;
            mealSchedule.StartDate = startDate;
            mealSchedule.EndDate = endDate;
            mealSchedule.LastEditUserId = lastEditorUserId;
            MealScheduleHandler.AddMealschedule(mealSchedule);

        }

        // Delete a mealschedule from database due to id
        public void DeleteMealSchedule(string id)
        {
            MealScheduleHandler.RemoveMealschedule(id);
            //dataHandler.RemoveMealschedule(id);
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditorUserId)
        {
            MealSchedule mealSchedule = new MealSchedule();
            //mealSchedule.Id = id;//commented after id is int

            mealSchedule.Topic = topic;
            mealSchedule.Participants = participants;
            mealSchedule.Location = location;
            mealSchedule.StartDate = startDate;
            mealSchedule.EndDate = endDate;
            mealSchedule.LastEditUserId = lastEditorUserId;
            //MealScheduleHandler.UpdateAMealschedule()
            dataHandler.UpdateAMealschedule(id, topic, participants, location, startDate, endDate, lastEditorUserId);
        }

        public MealSchedule FindMealScheduleById (string id)
        {
            return MealScheduleHandler.FindMealScheduleById(id);
            //return dataHandler.FindMealScheduleById(id);
        }

        // Find all meal schedules from database
        public List<MealSchedule> FindAllMealSchedules()
        {
            return MealScheduleHandler.FindAllMealSchedules();
        }
    }
}
