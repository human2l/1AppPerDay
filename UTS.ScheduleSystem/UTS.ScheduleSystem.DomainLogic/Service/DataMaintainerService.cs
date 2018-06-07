using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
using UTS.ScheduleSystem.DomainLogic.DataHandler;

namespace UTS.ScheduleSystem.DomainLogic
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
                Topic = topic.ToLower(),
                Participants = participants.ToLower(),
                Location = location.ToLower(),
                StartDate = startDate.ToLower(),
                EndDate = endDate.ToLower(),
                LastEditUserId = lastEditorUserId
            };
            MealScheduleHandler.AddMealschedule(mealSchedule);

        }

        // Delete a mealschedule from database due to id
        public void DeleteMealSchedule(string id)
        {
            MealScheduleHandler.RemoveMealschedule(id);
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string lastEditorUserId)
        {
            MealSchedule mealSchedule = new MealSchedule
            {
                Id = int.Parse(id),
                Topic = topic.ToLower(),
                Participants = participants.ToLower(),
                Location = location.ToLower(),
                StartDate = startDate.ToLower(),
                EndDate = endDate.ToLower(),
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

        // Check user inputs
        public bool IsDataValid(MealSchedule m)
        {
            return (Utils.IsStringValid(m.Topic) &&
                Utils.IsStringValid(m.Location) &&
                Utils.IsStringValid(m.Participants) &&
                Utils.IsStringValid(m.StartDate) &&
                Utils.IsStringValid(m.EndDate));
        }
    }
}
