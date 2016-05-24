using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using lunch.BusinessLogic.Security;

namespace lunch.Api.Internal.Auth
{
    internal class ApplicationJwtBearerEvents : IJwtBearerEvents
    {
        public Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return Task.CompletedTask;
        }

        public Task Challenge(JwtBearerChallengeContext context)
        {
            return Task.CompletedTask;
        }

        public Task MessageReceived(MessageReceivedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task TokenValidated(TokenValidatedContext context)
        {
            var userSessionBusinessLogic = context.HttpContext.RequestServices.GetService<IUserSessionBusinessLogic>();

            var isValid = await userSessionBusinessLogic.GetIsPrincipalValid(context.Ticket.Principal);
            if (isValid)
                context.HandleResponse();
        }
    }
}
