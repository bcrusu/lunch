using lunch.Api.Internal.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
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

        private static void ConfigureMvcJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureMvcOptions(MvcOptions options)
        {
            options.Filters.Add(new AutoSaveChangesAttribute());
        }
    }
}
