using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.initialization();
            System.Diagnostics.Debug.WriteLine(controller.FakeDB.UserTbl);

            System.Diagnostics.Debug.WriteLine(controller.Utils.CreateIdByType("user",controller.UserList));
            
        }
    }
}
