using System;
using lunch.Domain.Security;

namespace lunch.BusinessLogic.Security
{
    public interface IUserSessionBusinessLogic
    {
        UserSession FindSession(Guid token);

        UserSession CreateSession(User user);

        bool GetIsUserSessionValid(Guid token);
    }
}
