using lunch.Api.Internal.Auth;
using lunch.Api.Internal.Cors;
using lunch.Api.Internal.Mvc;
using lunch.BusinessLogic;
using lunch.Configuration;
using lunch.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace lunch.Api
{
    public class Startup
    {
        private readonly string _contentRootPath;
        private readonly string _environmentName;

        public Startup(IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;
            _environmentName = env.EnvironmentName;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(_contentRootPath, "appsettings", _environmentName);
            services.AddRepositories();
            services.AddBusinessLogic();

            services.AddMvcServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.UseCors();

            app.UseJwtAuthentication();           

            app.UseMvc();
        }
    }
}
