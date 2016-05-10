using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using lunch.Configuration;

namespace lunch.Api.Auth
{
    internal class JwtHelper
    {
        public const string Issuer = "https://lunch.com/";
        public const string ClientId = "lunch";
        public const string SignatureAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
        public const int ExpireDays = 14;

        public static string Create(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var notBefore = DateTime.UtcNow;
            var expires = notBefore.AddDays(ExpireDays);

            var jwtSecurityToken = tokenHandler.CreateToken(Issuer, ClientId, claimsIdentity, notBefore, expires, GetSigningCredentials());

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        private static SigningCredentials GetSigningCredentials()
        {
            var securityKey = new InMemorySymmetricSecurityKey(ApplicationSettings.JwtSignKey);
            return new SigningCredentials(securityKey, SignatureAlgorithm, "not_used");
        }
    }
}