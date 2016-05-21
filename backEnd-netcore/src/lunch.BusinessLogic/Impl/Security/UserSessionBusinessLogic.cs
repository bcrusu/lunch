using System;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using lunch.Repositories.Security;
using System.Threading.Tasks;

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

        public async Task<bool> GetIsUserSessionValid(Guid token)
        {
            var userSession = await _userSessionRepository.FindByToken(token);
            if (userSession == null)
                return false;

            if (userSession.State != UserSessionState.Active)
                return false;

            var expireDate = userSession.CreationDate.AddDays(ApplicationConstants.UserSessionExpireDays);
            if (expireDate < DateTime.UtcNow)
                return false;

            return true;
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
                if (activeSessionsCount >= ApplicationConstants.MaxActiveSessionsCount)
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
    }
}
