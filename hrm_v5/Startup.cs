using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hrm_v5.Startup))]
namespace hrm_v5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
