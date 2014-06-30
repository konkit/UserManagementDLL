using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketPlace.Startup))]
namespace MarketPlace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
