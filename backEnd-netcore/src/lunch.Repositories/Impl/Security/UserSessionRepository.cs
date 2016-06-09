using System;
using System.Linq;
using lunch.Domain.Security;
using lunch.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lunch.Repositories.Impl.Security
{
    internal class UserSessionRepository : RepositoryBase<UserSession, Guid>, IUserSessionRepository
    {
        public UserSessionRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {            
        }

        public virtual Task<UserSession> FindByToken(Guid token)
        {
            return Set.Include(x => x.User).SingleOrDefaultAsync(x => x.Token == token);
        }

        public int GetActiveUserSessionsCount(int userId)
        {
            return Set.Include(x => x.User).Count(x => x.UserId == userId && x.State == UserSessionState.Active);
        }
    }
}
