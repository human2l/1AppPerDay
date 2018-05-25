using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.Data;

namespace UTS.ScheduleSystem.MainLogic.DatabaseHandler
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

        public static string getCurrentUserRole(string userName)
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
