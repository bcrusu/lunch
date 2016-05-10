using System.Linq;
using lunch.Domain.Security;
using lunch.Repositories.Security;

namespace lunch.Repositories.Impl.Security
{
    internal class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        
        public User FindByEmail(string email)
        {
            //TODO: enforce case-insensitivity for Email column
            return Set.SingleOrDefault(x => x.Email == email);
        }
    }
}
