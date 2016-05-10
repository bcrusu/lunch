using System.Web.Http;
using lunch.Api.Unity;
using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebApiActivator), "Shutdown")]

namespace lunch.Api.Unity
{
    public static class UnityWebApiActivator
    {
        public static void Start() 
        {
            var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetContainer());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            var container = UnityConfig.GetContainer();
            container.Dispose();
        }
    }
}
