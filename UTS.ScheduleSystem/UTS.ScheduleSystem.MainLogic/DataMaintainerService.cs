using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public class DataMaintainerService
    {

        public void addMealSchedule(MealSchedule mealSchedule, ref List<MealSchedule> mealScheduleList)
        {

            //for datamaintainer to add meal
            mealScheduleList.Add(mealSchedule);
        }

        //public void getMealSchedules(List<MealSchedule> mealScheduleList)
        //{
        //    //for datamaintainer to list all meal schedule
        //    //note: parameter mealSchedulelist will convert to xml then send to front end


        //}

        public List<MealSchedule> deleteMealSchedule(string id, List<MealSchedule> mealScheduleList)
        {
            //for datamaintainer to delete meal from mealschedule list
            foreach(MealSchedule mealSchedule in mealScheduleList)
            {
                if(mealSchedule.Id == id)
                {
                    mealScheduleList.Remove(mealSchedule);
                    return mealScheduleList;
                }
            }
            return mealScheduleList;

        }

        public List<MealSchedule> updateMealSchedule(MealSchedule ms, List<MealSchedule> mealScheduleList)
        {
            System.Diagnostics.Debug.WriteLine(ms);
            for(int i =0; i < mealScheduleList.Count; i++)
            {
                if(mealScheduleList[i].Id == ms.Id)
                {
                    System.Diagnostics.Debug.WriteLine("Updated record!");
                    mealScheduleList[i] = ms;
                }
            }
            return mealScheduleList;
        }


    }
}
