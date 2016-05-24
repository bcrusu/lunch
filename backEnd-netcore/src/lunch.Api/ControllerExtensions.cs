using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace lunch.Api
{
    internal static class ControllerExtensions
    {
        public static void CheckModelStateIsValid(this Controller target)
        {
            if (!target.ModelState.IsValid)
                throw new Exception();
                // TODO: throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public static Task<UserSession> GetCurrentUserSession(this Controller target)
        {
            var claimsPrincipal = target.User;
            if (claimsPrincipal == null)
                return null;

            var userSessionBusinessLogic = target.Request.HttpContext.RequestServices.GetService<IUserSessionBusinessLogic>();
            return userSessionBusinessLogic.GetUserSessionForPrincipal(claimsPrincipal);            
        }
    }
}
