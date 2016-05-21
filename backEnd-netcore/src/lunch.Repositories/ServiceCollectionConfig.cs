using lunch.Repositories.Impl;
using lunch.Repositories.Impl.Security;
using lunch.Repositories.Security;
using Microsoft.Extensions.DependencyInjection;

namespace lunch.Repositories
{
    public static class ServiceCollectionConfig
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<IDbContextOperations, ApplicationDbContextOperations>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserSessionRepository, UserSessionRepository>();
        }
    }
}
