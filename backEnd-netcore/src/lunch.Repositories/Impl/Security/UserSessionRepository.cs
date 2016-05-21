using System;
using System.Linq;
using lunch.Domain.Security;
using lunch.Repositories.Security;

namespace lunch.Repositories.Impl.Security
{
    internal class UserSessionRepository : RepositoryBase<UserSession, Guid>, IUserSessionRepository
    {
        public int GetActiveUserSessionsCount(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
