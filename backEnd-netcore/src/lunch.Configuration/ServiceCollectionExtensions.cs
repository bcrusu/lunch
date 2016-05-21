using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lunch.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfiguration(this IServiceCollection services,
            string baseBath, string fileName, string environmentName = null)
        {
            var configurationRoot = CreateConfigurationRoot(baseBath, fileName, environmentName);

            services.AddTransient<IApplicationSettings, ApplicationSettings>(x => new ApplicationSettings(configurationRoot));
        }

        private static IConfigurationRoot CreateConfigurationRoot(string baseBath, string fileName, string environmentName = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(baseBath)
                .AddJsonFile($"{fileName}.json", optional: true, reloadOnChange: true);

            if (!string.IsNullOrEmpty(environmentName))
                builder.AddJsonFile($"{fileName}.{environmentName}.json", optional: true);

            builder.AddUserSecrets("Lunch")  // ~\AppData\Roaming\Microsoft\UserSecrets\{Lunch}\secrets.json
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
