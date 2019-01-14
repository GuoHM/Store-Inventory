using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryWeb.Startup))]
namespace InventoryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
