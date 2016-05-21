using System;
using lunch.Domain.Security;

namespace lunch.Repositories.Security
{
    public interface IUserSessionRepository : IRepository<UserSession, Guid>
    {
        int GetActiveUserSessionsCount(int userId);
    }
}
