using lunch.BusinessLogic.Impl.Security;
using lunch.BusinessLogic.Security;
using Microsoft.Extensions.DependencyInjection;

namespace lunch.BusinessLogic
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IUserBusinessLogic, UserBusinessLogic>();
            services.AddTransient<IUserSessionBusinessLogic, UserSessionBusinessLogic>();
        }
    }
}
