using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic.DatabaseHandler
{
    public class UserHandler
    {
        public static List<AspNetUser> UsersList()
        {
            List<AspNetUser> users;
            using (ScheduleSystemContext context = new ScheduleSystemContext())
            {
                /*users = (from AspNetUser
                           in context.AspNetUsers
                           select AspNetUser).ToList();*/
                users = new List<AspNetUser>();
            }
            return users;
        }

    }
}
