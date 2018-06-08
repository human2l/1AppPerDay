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
