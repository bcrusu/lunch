using System;
using lunch.Domain.Security;
using System.Threading.Tasks;

namespace lunch.Repositories.Security
{
    public interface IUserSessionRepository : IRepository<UserSession, Guid>
    {
        Task<UserSession> FindByToken(Guid token);

        int GetActiveUserSessionsCount(int userId);
    }
}
