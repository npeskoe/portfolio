using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CashHomeBuyer.Startup))]
namespace CashHomeBuyer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
