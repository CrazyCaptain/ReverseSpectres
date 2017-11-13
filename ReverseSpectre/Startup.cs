using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReverseSpectre.Startup))]
namespace ReverseSpectre
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
