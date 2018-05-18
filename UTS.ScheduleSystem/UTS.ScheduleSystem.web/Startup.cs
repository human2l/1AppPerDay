using Microsoft.Owin;
using Owin;
using System;
using System.IO;

[assembly: OwinStartupAttribute(typeof(UTS.ScheduleSystem.Web.Startup))]
namespace UTS.ScheduleSystem.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {

            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\UTS.ScheduleSystem.Data"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            ConfigureAuth(app);
        }
    }
}
