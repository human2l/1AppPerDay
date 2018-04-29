using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public static class Utils
    {
        //create if for different objects
        //params: String:type of object, List: the list which contains that type of objects
        public static string CreateIdByType<T>(string objType, List<T> list)
        {
            switch (objType)
            {
                case "User":
                    //Currently it doesn't need to create id for user
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

        //return the object has the larest ID
        public static string GetLastId<T>(List<T> list)
        {
            if(typeof(T).Equals(typeof(User)))
            {
                if(list.Count == 0)
                {
                    return "u0";
                }
                var largestIdNumber = 0;
                List<User> userList = list.Cast<User>().ToList();
                for(var i = 0; i < userList.Count; i++)
                {
                    var idNumber = int.Parse(userList[i].Id.Substring(1));
                    if (idNumber > largestIdNumber)
                    {
                        largestIdNumber = idNumber;
                    }
                }
                return "u" + largestIdNumber;
            }else if (typeof(T).Equals(typeof(ConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "c0";
                }
                var largestIdNumber = 0;
                List<ConversationalRule> conversationalRuleList = list.Cast<ConversationalRule>().ToList();
                for (var i = 0; i < conversationalRuleList.Count; i++)
                {
                    var idNumber = int.Parse(conversationalRuleList[i].Id.Substring(1));
                    if (idNumber > largestIdNumber)
                    {
                        largestIdNumber = idNumber;
                    }
                }
                return "c" + largestIdNumber;
            }
            else if (typeof(T).Equals(typeof(FixedConversationalRule)))
            {
                if(list.Count == 0)
                {
                    return "fc0";
                }
                var largestIdNumber = 0;
                List<FixedConversationalRule> fixedConversationalRuleList = list.Cast<FixedConversationalRule>().ToList();
                for (var i = 0; i < fixedConversationalRuleList.Count; i++)
                {
                    var idNumber = int.Parse(fixedConversationalRuleList[i].Id.Substring(2));
                    if (idNumber > largestIdNumber)
                    {
                        largestIdNumber = idNumber;
                    }
                }
                return "fc" + largestIdNumber;
            }
            else if (typeof(T).Equals(typeof(MealSchedule)))
            {
                if(list.Count == 0)
                {
                    return "ms0";
                }
                var largestIdNumber = 0;
                List<MealSchedule> mealScheduleList = list.Cast<MealSchedule>().ToList();
                for (var i = 0; i < mealScheduleList.Count; i++)
                {
                    var idNumber = int.Parse(mealScheduleList[i].Id.Substring(2));
                    if (idNumber > largestIdNumber)
                    {
                        largestIdNumber = idNumber;
                    }
                }
                return "ms" + largestIdNumber;
            }
                return null;
        }

        //return corresponding Role of input string
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
