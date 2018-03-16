using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntegratieProject_PoC.Startup))]
namespace IntegratieProject_PoC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
