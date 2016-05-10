using lunch.Configuration;
using Microsoft.Owin.Security;
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
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { JwtHelper.ClientId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(JwtHelper.Issuer, ApplicationSettings.JwtSignKey)
                }
            };

            app.UseJwtBearerAuthentication(authenticationOptions);
        }
    }
}
