using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoeCollectionMVC.Startup))]
namespace ShoeCollectionMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
