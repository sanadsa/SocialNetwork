using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client_SocialNetwork.Startup))]
namespace Client_SocialNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
