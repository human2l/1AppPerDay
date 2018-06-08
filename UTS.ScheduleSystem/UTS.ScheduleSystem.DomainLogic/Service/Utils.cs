using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;
public enum Status { Approved, Rejected, Pending }

namespace UTS.ScheduleSystem.DomainLogic
{
    public enum Status { Approved, Rejected, Pending }

    public static class Utils
    {
        //create if for different objects
        //params: String:type of object, List: the list which contains that type of objects
        public static string CreateIdByType(string objType, string lastId)
        {
            switch (objType)
            {
                case "User":
                    //Currently it doesn't need to create id for user
                    //return "u" + (int.Parse(GetLastId(list).Substring(1))+1);
                    return "uuu";
                case "ConversationalRule":
                    return (lastId == null) ? "c1" : "c" + (int.Parse(lastId.Substring(1)) + 1);
                case "FixedConversationalRule":
                    return (lastId == null) ? "fc1" : "fc" + (int.Parse(lastId.Substring(2)) + 1);
                case "MealSchedule":
                    return (lastId == null) ? "ms1" : "ms" + (int.Parse(lastId.Substring(2)) + 1);
                default:
                    return null;
            }
        }

        // Find last editor id from relatedUsersId string
        public static string FindLastEditorId(string relatedUsersId)
        {
            string[] usersId = relatedUsersId.Split(' ');
            return usersId[usersId.Count() - 1];
        }

        //Convert status string to status enum
        public static Status GetStatus(string status)
        {
            switch (status)
            {
                case "Approved":
                    return Status.Approved;
                case "Rejected":
                    return Status.Rejected;
                default:
                    return Status.Pending;
            }
        }
        
        //convert multiple white space in a string to a single white space
        public static string IgnoreSpace(string input)
        {
            char[] WhiteSpace = new char[] { ' ' };
            string longString = input;
            string[] split = longString.Split(WhiteSpace, StringSplitOptions.RemoveEmptyEntries);
            string compactedString = string.Join(" ", split);
            return compactedString;
        }

        // Remove all marks
        public static string RemoveAllMarks(string input)
        {
            string pattern = @"[^0-9a-zA-Z\s]";
            return Regex.Replace(input, pattern, "");
        }

        // Check if input is not null and includes only alphabet and num (判断只包含数字字母 并且不为空)
        public static bool IsStringValid(string input)
        {
            string pattern = @"^[a-zA-Z0-9\s]*$";
            return ((input != null) && (Regex.IsMatch(input, pattern)));
        }

    }
}
