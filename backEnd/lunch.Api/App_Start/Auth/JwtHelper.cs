using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using lunch.Configuration;
using lunch.Domain.Security;

namespace lunch.Api.Auth
{
    internal class JwtHelper
    {
        public const string Issuer = "lunch.com";
        public const string ClientId = "lunch";
        public const string SignatureAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        public const int ExpireDays = 14;
        private const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";

        public static string Create(UserSession userSession)
        {
            var claimsIdentity = CreateClaimsIdentity(userSession);

            var tokenHandler = new JwtSecurityTokenHandler();

            var notBefore = DateTime.UtcNow;
            var expires = notBefore.AddDays(ExpireDays);

            var jwtSecurityToken = tokenHandler.CreateToken(Issuer, ClientId, claimsIdentity, notBefore, expires, GetSigningCredentials());

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Token.ToString("N"), StringValueType, Issuer)
            };

            return new ClaimsIdentity(claims, ClientId);
        }

        private static SigningCredentials GetSigningCredentials()
        {
            var securityKey = new InMemorySymmetricSecurityKey(ApplicationSettings.JwtSignKey);
            return new SigningCredentials(securityKey, SignatureAlgorithm, "not_used");
        }
    }
}