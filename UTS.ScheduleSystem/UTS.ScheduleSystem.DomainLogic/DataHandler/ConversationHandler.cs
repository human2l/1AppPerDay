using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.DataHandler
{
    class ConversationHandler
    {
        public static string GetOutputByInput(string input, string inputKeyword, string answerKeyword)
        {
            MealSchedule mealSchedule;
            switch (inputKeyword)
            {
                case "topic":
                    mealSchedule = GetOutputByTopic(input);
                    break;
                case "participants":
                    mealSchedule = GetOutputByParticipants(input);
                    break;
                case "location":
                    mealSchedule = GetOutputByLocation(input);
                    break;
                case "startdate":
                    mealSchedule = GetOutputByStartDate(input);
                    break;
                case "enddate":
                    mealSchedule = GetOutputByEndDate(input);
                    break;
                default:
                    mealSchedule = null;
                    break;
            }
            if(mealSchedule != null)
            {
                switch (answerKeyword)
                {
                    case "topic":
                        return mealSchedule.Topic;
                    case "participants":
                        return mealSchedule.Participants;
                    case "location":
                        return mealSchedule.Location;
                    case "startdate":
                        return mealSchedule.StartDate;
                    case "enddate":
                        return mealSchedule.EndDate;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        private static MealSchedule GetOutputByTopic(string input)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Topic == input
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }

        private static MealSchedule GetOutputByParticipants(string input)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Participants == input
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }
        private static MealSchedule GetOutputByLocation(string input)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.Location == input
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }
        private static MealSchedule GetOutputByStartDate(string input)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.StartDate == input
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }
        private static MealSchedule GetOutputByEndDate(string input)
        {
            MealSchedule mealSchedule;
            try
            {
                using (ScheduleSystemContext context = new ScheduleSystemContext())
                {
                    mealSchedule = (from MealSchedule
                                    in context.MealSchedules
                                    where MealSchedule.EndDate == input
                                    select MealSchedule).First();
                }
            }
            catch
            {
                mealSchedule = null;
            }
            return mealSchedule;
        }
    }
}
