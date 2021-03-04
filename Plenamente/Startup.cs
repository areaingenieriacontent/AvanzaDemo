using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Plenamente.Startup))]
namespace Plenamente
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
