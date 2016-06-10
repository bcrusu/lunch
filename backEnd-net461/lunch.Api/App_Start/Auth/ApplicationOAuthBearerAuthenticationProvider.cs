using System;
using System.Threading.Tasks;
using lunch.Api.Unity;
using lunch.BusinessLogic.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;

namespace lunch.Api.Auth
{
    public class ApplicationOAuthBearerAuthenticationProvider : IOAuthBearerAuthenticationProvider
    {
        public Task RequestToken(OAuthRequestTokenContext context)
        {
            return Task.CompletedTask;
        }

        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            context.Rejected();

            Guid userSessionToken;
            if (JwtHelper.TryGetUserSessionToken(context.Ticket.Identity, out userSessionToken))
            {
                using (var container = UnityConfig.GetContainer().CreateChildContainer())
                {
                    var userSessionBusinessLogic = (IUserSessionBusinessLogic) container.Resolve(typeof(IUserSessionBusinessLogic));

                    var isValid = userSessionBusinessLogic.GetIsUserSessionValid(userSessionToken);
                    if (isValid)
                        context.Validated();
                }
            }

            return Task.CompletedTask;
        }

        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return Task.CompletedTask;
        }
    }
}