using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoC_UI.Startup))]
namespace PoC_UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
