using lunch.Repositories.Impl.Security;
using lunch.Repositories.Security;
using Microsoft.Practices.Unity;

namespace lunch.Repositories
{
    public static class UnityConfig
    {
        public static void RegisterRepositoryTypes(this IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
        }
    }
}
