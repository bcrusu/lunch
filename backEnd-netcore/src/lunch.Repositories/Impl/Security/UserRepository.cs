using System;
using lunch.Domain.Security;
using lunch.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lunch.Repositories.Impl.Security
{
    internal class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual Task<User> FindById(int id)
        {
            return Set.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<User> FindByEmail(string email)
        {
            return Set.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
