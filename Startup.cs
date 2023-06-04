using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LandscapeArchitectsApplication.Startup))]
namespace LandscapeArchitectsApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
