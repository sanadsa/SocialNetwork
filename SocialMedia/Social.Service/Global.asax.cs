using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Social.Common.Interfaces;
using Social.DAL;
using SocialBL;
using System.Web.Http;

namespace Social.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IUserManager, UserManager>(Lifestyle.Scoped);
            container.Register<IPostManager, PostManager>(Lifestyle.Scoped);
            container.Register<IPostRepository, PostRepository>(Lifestyle.Scoped);
            container.Register<IFeedRepository, FeedRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
