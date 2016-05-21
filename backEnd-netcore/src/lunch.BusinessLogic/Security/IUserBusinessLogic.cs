using lunch.Domain.Security;
using System.Threading.Tasks;

namespace lunch.BusinessLogic.Security
{
    public interface IUserBusinessLogic
    {
        Task<User> FindByEmail(string email);

        User CreateUser(ExternalUserDetails externalUserDetails);
    }
}
