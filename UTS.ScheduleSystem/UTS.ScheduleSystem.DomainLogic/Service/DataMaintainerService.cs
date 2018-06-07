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
        public void AddMealSchedule(MealSchedule mealSchedule)
        {
            MealScheduleHandler.AddMealschedule(mealSchedule);

        }

        // Delete a mealschedule from database due to id
        public void DeleteMealSchedule(string id)
        {
            MealScheduleHandler.RemoveMealschedule(id);
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(MealSchedule mealSchedule)
        {
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

        // Format mealschedule inputs
        public MealSchedule MealScheduleFormat(MealSchedule mealSchedule)
        {
            MealSchedule newMealSchedule = mealSchedule;
            newMealSchedule.Topic = Utils.IgnoreSpace(mealSchedule.Topic.ToLower());
            newMealSchedule.Location = Utils.IgnoreSpace(mealSchedule.Location.ToLower());
            newMealSchedule.Participants = Utils.IgnoreSpace(mealSchedule.Participants.ToLower());
            newMealSchedule.StartDate = Utils.IgnoreSpace(mealSchedule.StartDate.ToLower());
            newMealSchedule.EndDate = Utils.IgnoreSpace(mealSchedule.EndDate.ToLower());
            return newMealSchedule;
        }
    }
}
