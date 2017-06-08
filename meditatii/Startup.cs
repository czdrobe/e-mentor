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
        }
    }
}
