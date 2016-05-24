using lunch.Configuration;
using lunch.Domain.Security;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using lunch.BusinessLogic.Security;

namespace lunch.BusinessLogic.Impl.Security
{
    internal class JwtSecurityTokenFactory : IJwtSecurityTokenFactory
    {
        private readonly IApplicationSettings _applicationSettings;

        public JwtSecurityTokenFactory(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public string Create(UserSession userSession)
        {
            var claimsIdentity = CreateClaimsIdentity(userSession);

            var tokenHandler = new JwtSecurityTokenHandler();

            var issuedAt = DateTime.UtcNow;
            var notBefore = issuedAt;
            var expires = notBefore.AddDays(ApplicationConstants.Sessions.ExpireDays);

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                ApplicationConstants.Jwt.Issuer, ApplicationConstants.Jwt.Audience,
                claimsIdentity, notBefore, expires, issuedAt, GetSigningCredentials());

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(ApplicationConstants.Jwt.UserSessionTokenClaimType,
                userSession.Token.ToString(ApplicationConstants.Jwt.UserSessionTokenStringFormat),
                ApplicationConstants.XmlSchema.StringValueType,
                ApplicationConstants.Jwt.Issuer)
            };

            return new ClaimsIdentity(claims, "JWT");
        }

        private SigningCredentials GetSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(_applicationSettings.JwtSignKey);
            return new SigningCredentials(securityKey, ApplicationConstants.Jwt.SignatureAlgorithm);
        }
    }
}
