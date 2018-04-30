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

        public void AddMealSchedule(string topic, string participants, string location, string startDate, string endDate, string lastEditorUserId)
        {

            //for datamaintainer to add meal
            MealSchedule ms = new MealSchedule(Utils.CreateIdByType("MealSchedule", dataHandler.FindLastMealscheduleId()), topic, participants, location, startDate, endDate, lastEditorUserId);
            dataHandler.AddMealschedule(ms.Id, ms.Topic, ms.Participants, ms.Location, ms.StartDate, ms.EndDate, ms.LastEditUserId);
            //mealScheduleList.Add(mealSchedule);

        }

        //public void Add(MealSchedule mealSchedule)
        //{
        //    string[] newMealSchedule = { mealSchedule.Id, mealSchedule.Topic, mealSchedule.Participants, mealSchedule.Location, mealSchedule.StartDate, mealSchedule.EndDate, mealSchedule.LastEditUserId };
        //    databaseAdapter.AddMealSchedule(newMealSchedule);
        //}

        //public void getMealSchedules(List<MealSchedule> mealScheduleList)
        //{
        //    //for datamaintainer to list all meal schedule
        //    //note: parameter mealSchedulelist will convert to xml then send to front end


        //}

        public void DeleteMealSchedule(string id)
        {
            //for datamaintainer to delete meal from mealschedule list
            dataHandler.DeleteMealschedule(id);
            //foreach(MealSchedule mealSchedule in mealScheduleList)
            //{
            //    if(mealSchedule.Id == id)
            //    {
            //        mealScheduleList.Remove(mealSchedule);
            //        return mealScheduleList;
            //    }
            //}
            //return mealScheduleList;
        }

        //public List<MealSchedule> UpdateMealSchedule(MealSchedule ms, List<MealSchedule> mealScheduleList)
        //{
        //    System.Diagnostics.Debug.WriteLine(ms);
        //    for(int i =0; i < mealScheduleList.Count; i++)
        //    {
        //        if(mealScheduleList[i].Id == ms.Id)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Updated record!");
        //            mealScheduleList[i] = ms;
        //        }
        //    }
        //    return mealScheduleList;
        //}

        public void EditMealSchedule(string id, string topic, string participants, string location, string startDate, string endDate, string laseEditor)
        {
            //System.Diagnostics.Debug.WriteLine(ms);
            dataHandler.ChangeOnMealschedule(id, topic, participants, location, startDate, endDate, laseEditor);
            //for (int i = 0; i < mealScheduleList.Count; i++)
            //{
            //    if (mealScheduleList[i].Id == id)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Updated record!");
            //        mealScheduleList[i].Topic = topic;
            //        mealScheduleList[i].Participants = participants;
            //        mealScheduleList[i].Location = location;
            //        mealScheduleList[i].StartDate = startDate;
            //        mealScheduleList[i].EndDate = endDate;
            //    }
            //}
            //return mealScheduleList;
        }

        public List<MealSchedule> RequestMealScheduleList()
        {
            return dataHandler.FindMealSchedules();
        }
    }
}
