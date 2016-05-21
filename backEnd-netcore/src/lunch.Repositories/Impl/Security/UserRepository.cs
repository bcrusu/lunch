using System;
using lunch.Domain.Security;
using lunch.Repositories.Security;

namespace lunch.Repositories.Impl.Security
{
    internal class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public User FindByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
