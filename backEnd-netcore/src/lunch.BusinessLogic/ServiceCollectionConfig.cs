using lunch.BusinessLogic.Impl.Security;
using lunch.BusinessLogic.Security;
using Microsoft.Extensions.DependencyInjection;

namespace lunch.BusinessLogic
{
    public static class ServiceCollectionConfig
    {
        public static void ConfigureBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IUserBusinessLogic, UserBusinessLogic>();
            services.AddTransient<IUserSessionBusinessLogic, UserSessionBusinessLogic>();
        }
    }
}
