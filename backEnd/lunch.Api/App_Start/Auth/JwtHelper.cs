using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
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
        private const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
        private const string UserSessionTokenClaimType = ClaimTypes.Name;
        private const string UserSessionTokenStringFormat = "N";

        public static string Create(UserSession userSession)
        {
            var claimsIdentity = CreateClaimsIdentity(userSession);

            var tokenHandler = new JwtSecurityTokenHandler();

            var notBefore = DateTime.UtcNow;
            var expires = notBefore.AddDays(ApplicationConstants.UserSessionExpireDays);

            var jwtSecurityToken = tokenHandler.CreateToken(Issuer, ClientId, claimsIdentity, notBefore, expires, GetSigningCredentials());

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public static bool TryGetUserSessionToken(ClaimsIdentity identity, out Guid userSessionToken)
        {
            userSessionToken = Guid.Empty;

            if (identity == null)
                return false;

            var claims = identity.Claims.Where(MatchUserSessionClaim).ToList();
            if (claims.Count != 1)
                return false;

            return Guid.TryParseExact(claims[0].Value, UserSessionTokenStringFormat, out userSessionToken);
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(UserSessionTokenClaimType, userSession.Token.ToString(UserSessionTokenStringFormat), StringValueType, Issuer)
            };

            return new ClaimsIdentity(claims, "JWT");
        }

        private static SigningCredentials GetSigningCredentials()
        {
            var securityKey = new InMemorySymmetricSecurityKey(ApplicationSettings.JwtSignKey);
            return new SigningCredentials(securityKey, SignatureAlgorithm, "not_used");
        }

        private static bool MatchUserSessionClaim(Claim claim)
        {
            if (claim.Type != UserSessionTokenClaimType)
                return false;

            if (claim.Issuer != Issuer)
                return false;

            if (claim.ValueType != StringValueType)
                return false;

            return true;
        }
    }
}