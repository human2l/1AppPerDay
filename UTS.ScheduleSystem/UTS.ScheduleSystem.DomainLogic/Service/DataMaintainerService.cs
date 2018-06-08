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

        // Delete a mealschedule from database
        public void DeleteMealSchedule(MealSchedule mealSchedule)
        {
            MealScheduleHandler.RemoveMealschedule(mealSchedule.Id);
        }

        // Delete a mealschedule from database due to id
        public void DeleteMealScheduleById(string MealScheduleID)
        {
            MealScheduleHandler.RemoveMealschedule(int.Parse(MealScheduleID));
        }


        // Save edit on a mealschedule to database
        public void EditMealSchedule(MealSchedule mealSchedule)
        {
            MealScheduleHandler.UpdateAMealschedule(mealSchedule);
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
