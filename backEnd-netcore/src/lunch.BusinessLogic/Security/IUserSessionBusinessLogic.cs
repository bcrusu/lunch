using System;
using lunch.Domain.Security;
using System.Threading.Tasks;

namespace lunch.BusinessLogic.Security
{
    public interface IUserSessionBusinessLogic
    {
        Task<UserSession> FindSession(Guid token);
        
        Task<bool> GetIsUserSessionValid(Guid token);

        Task<UserSession> CreateSessionForExternalUser(ExternalUserDetails externalUserDetails);

        void CloseSession(UserSession userSession);
    }
}
