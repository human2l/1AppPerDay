using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.MainLogic.DatabaseHandler;

namespace UTS.ScheduleSystem.MainLogic
{
    public class DataMaintainerService
    {
        public DataMaintainerService()
        {

        }

        // Add a new mealschedule to database
        public void AddMealSchedule(string topic, string participants, string location, string startDate, string endDate, string lastEditorUserId)
        {

            //for datamaintainer to add meal
            MealSchedule mealSchedule = new MealSchedule
            {
                Topic = topic,
                Participants = participants,
                Location = location,
                StartDate = startDate,
                EndDate = endDate,
                LastEditUserId = lastEditorUserId
            };
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
            MealSchedule mealSchedule = new MealSchedule
            {
                Topic = topic,
                Participants = participants,
                Location = location,
                StartDate = startDate,
                EndDate = endDate,
                LastEditUserId = lastEditorUserId
            };
            MealScheduleHandler.UpdateAMealschedule(mealSchedule);
        }

        // Find a mealschedule by Id
        public MealSchedule FindMealScheduleById (string id)
        {
            return MealScheduleHandler.FindMealScheduleById(id);
        }

        // Find all meal schedules from database
        public List<MealSchedule> FindAllMealSchedules()
        {
            return MealScheduleHandler.FindAllMealSchedules();
        }
    }
}
