using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lunch.Api.Auth;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;

namespace lunch.Api
{
    internal static class WebApiExtensions
    {
        public static void CheckModelStateIsValid(this ApiController target)
        {
            if (!target.ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public static UserSession GetCurrentUserSession(this ApiController target)
        {
            var authenticationManager = target.Request.GetOwinContext().Authentication;

            var claimsPrincipal = authenticationManager.User;
            if (claimsPrincipal == null)
                return null;

            var userSessionToken = Guid.Empty;
            foreach (var claimsIdentity in claimsPrincipal.Identities)
                if (JwtHelper.TryGetUserSessionToken(claimsIdentity, out userSessionToken))
                    break;

            if (userSessionToken == Guid.Empty)
                return null;

            var userSessionBusinessLogic = (IUserSessionBusinessLogic)target.Request.GetDependencyScope().GetService(typeof(IUserSessionBusinessLogic));
            var userSession = userSessionBusinessLogic.FindSession(userSessionToken);

            return userSession;
        }
    }
}