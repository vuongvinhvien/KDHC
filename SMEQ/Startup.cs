using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMEQ.Startup))]
namespace SMEQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
