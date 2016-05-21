using lunch.Repositories.Impl;
using lunch.Repositories.Impl.Security;
using lunch.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace lunch.Repositories
{
    public static class ServiceCollectionConfig
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(ConfigureContextOptionsBuilder);
            services.AddTransient<IDbContextOperations, ApplicationDbContextOperations>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserSessionRepository, UserSessionRepository>();
        }

        private static void ConfigureContextOptionsBuilder(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("name=lunch");  //TODO: proper connection string config
        }
    }
}
