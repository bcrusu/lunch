using lunch.Domain.Security;

namespace lunch.BusinessLogic.Security
{
    public interface IJwtSecurityTokenFactory
    {
        string Create(UserSession userSession);
    }
}
