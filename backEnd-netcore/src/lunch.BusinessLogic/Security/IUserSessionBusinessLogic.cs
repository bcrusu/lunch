using System;
using lunch.Domain.Security;
using System.Threading.Tasks;
using System.Security.Claims;

namespace lunch.BusinessLogic.Security
{
    public interface IUserSessionBusinessLogic
    {
        Task<UserSession> FindSession(Guid token);
        
        Task<UserSession> CreateSessionForExternalUser(ExternalUserDetails externalUserDetails);

        void CloseSession(UserSession userSession);

        Task<bool> GetIsPrincipalValid(ClaimsPrincipal principal);

        Task<UserSession> GetUserSessionForPrincipal(ClaimsPrincipal principal);
    }
}
