using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamCode.Startup))]
namespace TeamCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
