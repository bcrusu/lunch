using System.Configuration;
using System.IO;

namespace lunch.Configuration
{
    public static class ApplicationSettings
    {
        private static string _linkedinClientSecret;

        public static string DefaultAccessControlAllowOrigin => ConfigurationManager.AppSettings["DefaultAccessControlAllowOrigin"];

        public static string LinkedinClientSecret
        {
            get
            {
                if (_linkedinClientSecret == null)
                    _linkedinClientSecret = File.ReadAllText(ConfigurationManager.AppSettings["LinkedinClientSecretFileName"]);

                return _linkedinClientSecret;
            }
        }
    }
}
