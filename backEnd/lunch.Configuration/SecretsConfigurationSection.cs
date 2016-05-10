using System.Configuration;

namespace lunch.Configuration
{
    public class SecretsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("LinkedinClientSecret", IsRequired = true)]
        public SecretElement LinkedinClientSecret
        {
            get { return (SecretElement)this["LinkedinClientSecret"]; }
            set { this["LinkedinClientSecret"] = value; }
        }

        [ConfigurationProperty("JwtSignKey", IsRequired = true)]
        public SecretElement JwtSignKey
        {
            get { return (SecretElement)this["JwtSignKey"]; }
            set { this["JwtSignKey"] = value; }
        }
    }
}
