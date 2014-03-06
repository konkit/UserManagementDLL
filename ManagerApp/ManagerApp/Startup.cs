using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagerApp.Startup))]
namespace ManagerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
