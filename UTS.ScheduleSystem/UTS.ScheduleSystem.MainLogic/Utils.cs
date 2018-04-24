using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem
{
    public static class Utils
    {
        public static string CreateIdByType<T>(string objType, List<T> list)
        {
            switch (objType)
            {
                case "User":
                    //return "u" + (int.Parse(GetLastId(list).Substring(1))+1);
                    return "uuu";
                case "ConversationalRule":
                    return "c" + (int.Parse(GetLastId(list).Substring(1)) + 1);
                case "FixedConversationalRule":
                    return "fc" + (int.Parse(GetLastId(list).Substring(2)) + 1);
                case "MealSchedule":
                    return "ms" + (int.Parse(GetLastId(list).Substring(2)) + 1);
                default:
                    return null;
            }
        }

        

        public static string GetLastId<T>(List<T> list)
        {
            if(typeof(T).Equals(typeof(User)))
            {
                if(list.Count == 0)
                {
                    return "u0";
                }
                User lastUser =(User)(object)list.Last();
                return lastUser.Id;
            }else if (typeof(T).Equals(typeof(ConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "c0";
                }
                ConversationalRule lastConversationalRule = (ConversationalRule)(object)list.Last();
                return lastConversationalRule.Id;
            }else if (typeof(T).Equals(typeof(FixedConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "fc0";
                }
                FixedConversationalRule lastFixedConversationalRule = (FixedConversationalRule)(object)list.Last();
                return lastFixedConversationalRule.Id;
            }else if (typeof(T).Equals(typeof(MealSchedule)))
            {
                if(list.Count == 0)
                {
                    return "ms0";
                }
                MealSchedule lastMealSchedule = (MealSchedule)(object)list.Last();
                return lastMealSchedule.Id;
            }
                return null;
        }

        public static Role GetRole(string role)
        {
            switch (role)
            {
                case "DMnEnA":
                    return Role.DMnEnA;
                case "DMnA":
                    return Role.DMnA;
                case "DMnE":
                    return Role.DMnE;
                case "EnA":
                    return Role.EnA;
                case "E":
                    return Role.E;
                case "A":
                    return Role.A;
                case "DM":
                    return Role.DM;

                default:
                    return Role.None;
            }
        }
    }
}
