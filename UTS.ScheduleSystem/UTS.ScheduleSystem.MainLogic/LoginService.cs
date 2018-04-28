using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    public class LoginService
    {
        public bool handleLogin(string loginInfo)
        {
            //search database 
            //if user exist , create user object and set current user then return true
            //return false if user doesn't exist

            return false;
        }

        public void handleLogout()
        {
            //wipe out current user
            
        }

        public void handleChangePassword()
        {
            //update current user password
            //update user  in  database
        }

    }
}
