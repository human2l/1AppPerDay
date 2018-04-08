using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class DMService
    {
        
        public void addMeal(string meal)
        {
            //for datamaintainer to add meal
        }

        public void getMealSchedule(List<MealSchedule> mealScheduleList)
        {
            //for datamaintainer to list all meal schedule
            //note: parameter mealSchedulelist will convert to xml then send to front end
            
        }

        public bool deleteMealSchedule(string meal)
        {
            //for datamaintainer to delete meal from mealschedule list
            return false;
        }

        
    }
}
