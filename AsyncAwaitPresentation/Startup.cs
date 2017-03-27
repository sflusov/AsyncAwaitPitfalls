using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsyncAwaitPresentation.Startup))]
namespace AsyncAwaitPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
