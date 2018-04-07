using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UTS.ScheduleSystem.web.Startup))]
namespace UTS.ScheduleSystem.web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
