using lunch.Repositories.Impl;
using lunch.Repositories.Impl.Security;
using lunch.Repositories.Security;
using Microsoft.Practices.Unity;

namespace lunch.Repositories
{
    public static class UnityConfig
    {
        public static void RegisterRepositoryTypes(this IUnityContainer container)
        {
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbContextOperations, ApplicationDbContextOperations>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserSessionRepository, UserSessionRepository>();
        }
    }
}
