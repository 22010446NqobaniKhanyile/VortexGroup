using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vortex.Startup))]
namespace Vortex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
