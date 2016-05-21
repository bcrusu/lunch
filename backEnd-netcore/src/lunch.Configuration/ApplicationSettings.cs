using System;

namespace lunch.Configuration
{
    public static class ApplicationSettings
    {
        public static string DefaultAccessControlAllowOrigin => "TODO";

        public static string LinkedinClientSecret => "TODO";

        public static byte[] JwtSignKey
        {
            get
            {
                var base64Key = "TODO";
                return Convert.FromBase64String(base64Key);
            }
        }
    }
}
