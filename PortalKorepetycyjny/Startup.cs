using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using PortalKorepetycyjny.Models;
using System.Web.Configuration;

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
