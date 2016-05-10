using System;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace lunch.Api.Auth
{
    public class AuthConfig
    {
        public static void Configure(IAppBuilder app)
        {
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
