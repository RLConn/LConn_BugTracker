using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LConn_BugTracker.Startup))]
namespace LConn_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
