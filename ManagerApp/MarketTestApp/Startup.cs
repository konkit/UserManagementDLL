using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketTestApp.Startup))]
namespace MarketTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
