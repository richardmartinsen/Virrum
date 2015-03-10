using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Virrum.Web.Startup))]
namespace Virrum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
