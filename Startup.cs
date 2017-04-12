using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OwnersPets.Startup))]
namespace OwnersPets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
