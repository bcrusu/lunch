using System.Security.Claims;

namespace lunch
{
    public static class ApplicationConstants
    {
        public static class Sessions
        {
            public const int ExpireDays = 14;
            public const int MaxActiveSessions = 50;
        }

        public static class Jwt
        {
            public const string Issuer = "lunch.com";
            public const string Audience = "lunch";
            public const string SignatureAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

            public const string UserSessionTokenClaimType = ClaimTypes.Name;
            public const string UserSessionTokenStringFormat = "N";
        }

        public static class XmlSchema
        {
            public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
        }
    }
}
