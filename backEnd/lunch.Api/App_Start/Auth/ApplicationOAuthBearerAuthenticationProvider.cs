using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace lunch.Api.Auth
{
    public class ApplicationOAuthBearerAuthenticationProvider : IOAuthBearerAuthenticationProvider
    {
        public Task RequestToken(OAuthRequestTokenContext context)
        {
            return Task.FromResult(0);
        }

        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            // TODO: verify is valid application user 
            return Task.FromResult(0);
        }

        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return Task.FromResult(0);
        }
    }
}