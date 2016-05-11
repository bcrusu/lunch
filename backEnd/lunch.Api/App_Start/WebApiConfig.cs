using System.Net.Http.Formatting;
using System.Web.Http;
using lunch.Api.Cors;
using lunch.Api.Unity;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity.WebApi;
using Newtonsoft.Json.Serialization;

namespace lunch.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetContainer());

            config.EnableCors(new DefaultCorsPolicyProvider());

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
 
            // Use only JSON messages
            config.Formatters.Clear();
            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Add(jsonFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
