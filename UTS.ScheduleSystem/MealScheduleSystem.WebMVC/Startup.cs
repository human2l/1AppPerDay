using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MealScheduleSystem.WebMVC.Startup))]
namespace MealScheduleSystem.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
