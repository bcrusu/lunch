using System;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace lunch.Api.Auth
{
    public class AuthConfig
    {
        public static void Configure(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            var authenticationOptions = new JwtBearerAuthenticationOptions
            {
                Provider = new ApplicationOAuthBearerAuthenticationProvider(),
                TokenHandler = new JwtSecurityTokenHandler
                {
                    TokenLifetimeInMinutes = (int) TimeSpan.FromDays(14).TotalMinutes
                }
            };

            app.UseJwtBearerAuthentication(authenticationOptions);
        }
    }
}
