using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelloSecurity.Startup))]
namespace HelloSecurity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
