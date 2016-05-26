using lunch.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace lunch.Api.Internal.Cors
{
    internal static class CorsConfig
    {
        public static void UseCors(this IApplicationBuilder app)
        {
            var applicationSettings = app.ApplicationServices.GetService<IApplicationSettings>();

            var allowedOrigin = applicationSettings.DefaultAccessControlAllowOrigin;

            app.UseCors(x => ConfigureCorsPolicy(x, allowedOrigin));
        }

        private static void ConfigureCorsPolicy(CorsPolicyBuilder builder, string allowedOrigin)
        {
            builder.AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader()
                .WithOrigins(allowedOrigin)
                .SetPreflightMaxAge(TimeSpan.FromMinutes(5));
        }
    }
}
