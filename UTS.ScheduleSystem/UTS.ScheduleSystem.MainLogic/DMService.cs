using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class DMService
    {

        public void addMealSchedule(MealSchedule mealSchedule, ref List<MealSchedule> mealScheduleList)
        {

            //for datamaintainer to add meal
            mealScheduleList.Add(mealSchedule);
        }

        public void getMealSchedules(List<MealSchedule> mealScheduleList)
        {
            //for datamaintainer to list all meal schedule
            //note: parameter mealSchedulelist will convert to xml then send to front end


        }

        public bool deleteMealSchedule(string id, List<MealSchedule> mealScheduleList)
        {
            //for datamaintainer to delete meal from mealschedule list
            foreach(MealSchedule mealSchedule in mealScheduleList)
            {
                if(mealSchedule.Id == id)
                {
                    mealScheduleList.Remove(mealSchedule);
                    return true;
                }
            }
            return false;

        }


    }
}
