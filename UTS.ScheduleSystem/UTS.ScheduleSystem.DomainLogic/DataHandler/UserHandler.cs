using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.DomainLogic.DataHandler
{
    public class UserHandler
    {
        public static List<AspNetUser> UsersList()
        {
            List<AspNetUser> users;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                users = (from AspNetUser
                           in context.AspNetUsers
                           select AspNetUser).ToList();
            }
            return users;
        }

        // Get current user role according to user name
        public static string GetCurrentUserRole(string userName)
        {
            foreach(var user in UsersList())
            {
                if(user.UserName == userName)
                {
                    return user.Role;
                }
            }
            return null;
        }
    }
}
