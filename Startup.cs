using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bod.Startup))]
namespace Bod
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
