using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRMWebApp.Startup))]
namespace CRMWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
