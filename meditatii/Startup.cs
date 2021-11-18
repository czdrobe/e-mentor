using meditatii.web.Utils;
using Meditatii.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(meditatii.Startup))]
namespace meditatii
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            AppStats.Current = new AppStats { TotalMeetingMinutes = 0, TotalTeacher = 0, TotalUser = 0 };
        }
    }
}
