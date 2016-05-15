using System;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using lunch.Repositories.Security;

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

        public UserSession FindSession(Guid token)
        {
            return _userSessionRepository.FindByKey(token);
        }

        public bool GetIsUserSessionValid(Guid token)
        {
            var userSession = _userSessionRepository.FindByKey(token);
            if (userSession == null)
                return false;

            if (userSession.State != UserSessionState.Active)
                return false;

            var expireDate = userSession.CreationDate.AddDays(ApplicationConstants.UserSessionExpireDays);
            if (expireDate < DateTime.UtcNow)
                return false;

            return true;
        }

        public UserSession CreateSessionForExternalUser(ExternalUserDetails externalUserDetails)
        {
            var user = _userBusinessLogic.FindByEmail(externalUserDetails.Email);
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

            return _userSessionRepository.Add(session);
        }

        public void CloseSession(UserSession userSession)
        {
            userSession.State = UserSessionState.Closed;
        }
    }
}
