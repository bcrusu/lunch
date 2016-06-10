using System;
using lunch.Domain.Security;

namespace lunch.BusinessLogic.Security
{
    public interface IUserSessionBusinessLogic
    {
        UserSession FindSession(Guid token);

        bool GetIsUserSessionValid(Guid token);

        UserSession CreateSessionForExternalUser(ExternalUserDetails externalUserDetails);

        void CloseSession(UserSession userSession);
    }
}
