using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UTS.ScheduleSystem.Web.Startup))]
namespace UTS.ScheduleSystem.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
