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
                    return "u" + (int.Parse(GetLastId(list).Substring(1))+1);
                case "ConversationalRule":
                    return "c" + (int.Parse(GetLastId(list).Substring(1)) + 1);
                case "FixedConversationalRule":
                    return "fc" + (int.Parse(GetLastId(list).Substring(1)) + 2);
                case "MealSchedule":
                    return "ms" + (int.Parse(GetLastId(list).Substring(1)) + 2);
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
                    return "u000";
                }
                User lastUser =(User)(object)list.Last();
                return lastUser.Id;
            }else if (typeof(T).Equals(typeof(ConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "c000";
                }
                ConversationalRule lastConversationalRule = (ConversationalRule)(object)list.Last();
                return lastConversationalRule.Id;
            }else if (typeof(T).Equals(typeof(FixedConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "fc000";
                }
                FixedConversationalRule lastFixedConversationalRule = (FixedConversationalRule)(object)list.Last();
                return lastFixedConversationalRule.Id;
            }else if (typeof(T).Equals(typeof(MealSchedule)))
            {
                if(list.Count == 0)
                {
                    return "ms000";
                }
                MealSchedule lastMealSchedule = (MealSchedule)(object)list.Last();
                return lastMealSchedule.Id;
            }
                return null;
        }
    }
}
