using System;
using System.Configuration;

namespace lunch.Configuration
{
    public static class ApplicationSettings
    {
        private static readonly SecretsConfigurationSection Secrets;

        static ApplicationSettings()
        {
            Secrets = (SecretsConfigurationSection)ConfigurationManager.GetSection("secrets");
        }

        public static string DefaultAccessControlAllowOrigin => ConfigurationManager.AppSettings["DefaultAccessControlAllowOrigin"];

        public static string LinkedinClientId => ConfigurationManager.AppSettings["LinkedinClientId"];

        public static string LinkedinClientSecret => Secrets.LinkedinClientSecret.Value;

        public static byte[] JwtSignKey
        {
            get
            {
                var base64Key = Secrets.JwtSignKey.Value;
                return Convert.FromBase64String(base64Key);
            }
        }
    }
}
