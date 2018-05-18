using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UTS.ScheduleSystem.Startup))]
namespace UTS.ScheduleSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
