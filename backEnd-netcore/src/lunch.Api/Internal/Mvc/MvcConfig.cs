using lunch.Api.Internal.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace lunch.Api.Internal.Mvc
{
    internal static class MvcConfig
    {
        public static void AddMvcServices(this IServiceCollection services)
        {
            services.Configure<MvcJsonOptions>(ConfigureMvcJsonOptions);

            services.AddMvc(ConfigureMvcOptions);
        }

        public static void UseApplicationMvc(this IApplicationBuilder app)
        {
            app.UseMvc(ConfigureMvcRoutes);
        }

        private static void ConfigureMvcJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureMvcOptions(MvcOptions options)
        {
            options.Filters.Add(CreateAuthorizeFilter());
            options.Filters.Add(new ExceptionLoggerFilter());
            options.Filters.Add(new AutoSaveChangesAttribute());
        }

        private static void ConfigureMvcRoutes(IRouteBuilder builder)
        {
            builder.MapRoute("default", "api/{controller}/{action}/{id?}");
        }

        private static AuthorizeFilter CreateAuthorizeFilter()
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            return new AuthorizeFilter(policy);
        }
    }
}
