using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortalKorepetycyjny.Startup))]
namespace PortalKorepetycyjny
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
