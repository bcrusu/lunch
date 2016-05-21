using System;
using lunch.Configuration;
using lunch.Repositories.Impl;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace lunch.Repositories.Migrations
{
    internal class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            var serviceProvider = GetServiceProvider();

            return new ApplicationDbContext(serviceProvider.GetService<IApplicationSettings>());
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddConfiguration(GetLunchApiProjectPath(), "appsettings", "Development");

            return services.BuildServiceProvider();
        }

        private static string GetLunchApiProjectPath()
        {
            var tmp = Directory.GetCurrentDirectory();
            tmp = Path.GetDirectoryName(tmp);
            return Path.Combine(tmp, "lunch.Api");
        }
    }
}
