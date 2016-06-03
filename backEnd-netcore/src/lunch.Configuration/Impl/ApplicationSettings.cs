using Microsoft.Extensions.Configuration;
using System;

namespace lunch.Configuration
{
    internal class ApplicationSettings : IApplicationSettings
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IConfigurationSection _appConfigurationSection;

        public ApplicationSettings(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            _appConfigurationSection = _configurationRoot.GetSection("Lunch");
        }

        public string DefaultAccessControlAllowOrigin => _appConfigurationSection["DefaultAccessControlAllowOrigin"];

        public byte[] JwtSignKey 
        {
            get
            {
                var base64Key = _appConfigurationSection["JwtSignKey"];
                if (string.IsNullOrEmpty(base64Key))
                    throw new InvalidOperationException("Invalid config value detected.");

                return Convert.FromBase64String(base64Key);
            }
        }

        public string LinkedinClientSecret => _appConfigurationSection["LinkedinClientSecret"];

        public string ConnectionString => _configurationRoot.GetConnectionString("Lunch");
    }
}
