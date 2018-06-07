using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.DataHandler
{
    public class MealScheduleHandler
    {
        // Add a mealschedule data into database
        public static void AddMealschedule(MealSchedule mealSchedule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                context.MealSchedules.Add(mealSchedule);
                context.SaveChanges();
            }
        }

        // Delete a mealschedule data from database
        public static void RemoveMealschedule(string Id)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var mealschedule = (from MealSchedule
                                            in context.MealSchedules
                                    where MealSchedule.Id == int.Parse(Id)
                                    select MealSchedule).First();
                context.MealSchedules.Remove(mealschedule);
                context.SaveChanges();
            }
        }

        // Delete a mealschedule data from data
        public static void RemoveMealschedule(MealSchedule onRemovingMealSchedule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var mealschedule = (from MealSchedule
                                            in context.MealSchedules
                                    where MealSchedule.Id == onRemovingMealSchedule.Id
                                    select MealSchedule).First();
                context.MealSchedules.Remove(mealschedule);
                context.SaveChanges();
            }
        }

        // Update a mealschedule data by Id
        public static void UpdateAMealschedule(MealSchedule mealschedule)
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var ms = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Id == mealschedule.Id
                                    select MealSchedule).First();
                ms.Topic = mealschedule.Topic;
                ms.Participants = mealschedule.Participants;
                ms.Location = mealschedule.Location;
                ms.StartDate = mealschedule.StartDate;
                ms.EndDate = mealschedule.EndDate;
                ms.LastEditUserId = mealschedule.LastEditUserId;
                context.SaveChanges();
            }
        }

        // Find meal sechdule by Id
        public static MealSchedule FindMealScheduleById(string id)
        {
            int intId = int.Parse(id);
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Id == intId
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }

        

        // Find all mealschedules from database
        public static List<MealSchedule> FindAllMealSchedules()
        {
            List<MealSchedule> mealSchedules;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedules = (from MealSchedule
                                     in context.MealSchedules
                                     select MealSchedule).ToList();
                }
            }
            catch
            {
                mealSchedules = null;
            }
            return mealSchedules;
        }

        // Remove all table rows
        public static bool ClearAllMealSchedule()
        {
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                var mealSchedules = context.MealSchedules.ToList();
                int count = mealSchedules.Count();
                int deleteCount = 0;
                foreach (var mealSchedule in mealSchedules)
                {
                    context.MealSchedules.Remove(mealSchedule);
                    deleteCount++;
                }
                return deleteCount == count;
            }
        }
    }
}

