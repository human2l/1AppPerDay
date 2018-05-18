using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UTS.ScheduleSystem.WebMVC.Startup))]
namespace UTS.ScheduleSystem.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
