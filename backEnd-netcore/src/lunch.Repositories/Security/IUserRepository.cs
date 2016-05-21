using lunch.Domain.Security;
using System.Threading.Tasks;

namespace lunch.Repositories.Security
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindById(int id);

        Task<User> FindByEmail(string email);
    }
}
