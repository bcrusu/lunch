using lunch.Domain.Security;

namespace lunch.BusinessLogic.Security
{
    public interface IUserBusinessLogic
    {
        User FindByEmail(string email);

        User CreateUser(ExternalUserDetails externalUserDetails);
    }
}
