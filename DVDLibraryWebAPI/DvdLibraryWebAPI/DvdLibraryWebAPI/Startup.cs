using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DvdLibraryWebAPI.Startup))]
namespace DvdLibraryWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
