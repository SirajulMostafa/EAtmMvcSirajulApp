using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EAtmMvcSirajulApp.Startup))]
namespace EAtmMvcSirajulApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
