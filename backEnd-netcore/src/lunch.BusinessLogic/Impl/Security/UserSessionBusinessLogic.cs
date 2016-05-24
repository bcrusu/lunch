using System;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using lunch.Repositories.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace lunch.BusinessLogic.Impl.Security
{
    internal class UserSessionBusinessLogic : IUserSessionBusinessLogic
    {
        private readonly IUserBusinessLogic _userBusinessLogic;
        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionBusinessLogic(IUserBusinessLogic userBusinessLogic, IUserSessionRepository userSessionRepository)
        {
            _userBusinessLogic = userBusinessLogic;
            _userSessionRepository = userSessionRepository;
        }

        public Task<UserSession> FindSession(Guid token)
        {
            return _userSessionRepository.FindByToken(token);
        }

        public async Task<UserSession> CreateSessionForExternalUser(ExternalUserDetails externalUserDetails)
        {
            var user = await _userBusinessLogic.FindByEmail(externalUserDetails.Email);
            if (user == null)
            {
                user = _userBusinessLogic.CreateUser(externalUserDetails);
            }
            else
            {
                if (user.Type != externalUserDetails.UserType)
                    throw new InvalidOperationException("Multiple sign-in providers are not supported for the same user.");

                // Check number of active sessions
                var activeSessionsCount = _userSessionRepository.GetActiveUserSessionsCount(user.Id);
                if (activeSessionsCount >= ApplicationConstants.Sessions.MaxActiveSessions)
                    throw new InvalidOperationException($"Max number of active sessions reached for user '{user.Id}'.");

                // Update user information
                user.FirstName = externalUserDetails.FirstName;
                user.LastName = externalUserDetails.LastName;
                user.DisplayName = externalUserDetails.DisplayName;
                user.PictureUrl = externalUserDetails.PictureUrl;
            }

            var session = new UserSession
            {
                Token = Guid.NewGuid(),  // TODO: use cryptographically random tokens
                State = UserSessionState.Active,
                CreationDate = DateTime.UtcNow,
                UserId = user.Id
            };

            _userSessionRepository.Add(session);
            return session;
        }

        public void CloseSession(UserSession userSession)
        {
            userSession.State = UserSessionState.Closed;
        }

        public async Task<bool> GetIsPrincipalValid(ClaimsPrincipal principal)
        {
            var userSession = await GetUserSessionForPrincipal(principal);
            if (userSession == null)
                return false;
 
            if (userSession.State != UserSessionState.Active)
                return false;

            var expireDate = userSession.CreationDate.AddDays(ApplicationConstants.Sessions.ExpireDays);
            if (expireDate < DateTime.UtcNow)
                return false;

            return true;
        }

        public Task<UserSession> GetUserSessionForPrincipal(ClaimsPrincipal principal)
        {
            if (principal == null)
                return Task.FromResult<UserSession>(null);

            foreach (var claimsIdentity in principal.Identities)
            {
                var claims = claimsIdentity.Claims.Where(MatchUserSessionClaim).ToList();
                if (claims.Count != 1)
                    continue;

                var userSessionToken = Guid.Empty;
                if (!Guid.TryParseExact(claims[0].Value, ApplicationConstants.Jwt.UserSessionTokenStringFormat, out userSessionToken))
                    continue;

                return _userSessionRepository.FindByToken(userSessionToken);
            }

            return Task.FromResult<UserSession>(null);
        }

        private static bool MatchUserSessionClaim(Claim claim)
        {
            if (claim.Type != ApplicationConstants.Jwt.UserSessionTokenClaimType)
                return false;

            if (claim.Issuer != ApplicationConstants.Jwt.Issuer)
                return false;

            if (claim.ValueType != ApplicationConstants.XmlSchema.StringValueType)
                return false;

            return true;
        }
    }
}
