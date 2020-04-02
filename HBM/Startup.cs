using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HBM.Startup))]
namespace HBM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
