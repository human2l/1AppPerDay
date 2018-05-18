using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.ScheduleSystem.MainLogic;

namespace RecreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\UTS.ScheduleSystem.Data"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            using (var context = new ScheduleSystemContext())
            {
                context.MealSchedules.ToList();
            }
        }
    }
}
