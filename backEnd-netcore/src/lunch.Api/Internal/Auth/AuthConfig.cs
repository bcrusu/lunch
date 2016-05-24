using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using lunch.Configuration;

namespace lunch.Api.Internal.Auth
{
    internal static class AuthConfig
    {
        public static void UseJwtAuthentication(this IApplicationBuilder app)
        {
            var applicationSettings = app.ApplicationServices.GetService<IApplicationSettings>();

            var options = new JwtBearerOptions();
            options.Audience = ApplicationConstants.Jwt.Audience;
            options.SaveToken = false;
            options.Events = new ApplicationJwtBearerEvents();
            options.TokenValidationParameters.ValidAudience = ApplicationConstants.Jwt.Audience;
            options.TokenValidationParameters.ValidIssuer = ApplicationConstants.Jwt.Issuer;
            options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(applicationSettings.JwtSignKey); 
            
            app.UseJwtBearerAuthentication(options);
        }
    }
}
