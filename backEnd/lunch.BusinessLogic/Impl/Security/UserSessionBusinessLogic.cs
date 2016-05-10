using System;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using lunch.Repositories.Security;

namespace lunch.BusinessLogic.Impl.Security
{
    internal class UserSessionBusinessLogic : IUserSessionBusinessLogic
    {
        private const int MaxActiveSessionsCount = 5;

        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionBusinessLogic(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public UserSession FindSession(Guid token)
        {
            return _userSessionRepository.FindByKey(token);
        }

        public UserSession CreateSession(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var activeSessionsCount = _userSessionRepository.GetActiveUserSessionsCount(user.Id);
            if (activeSessionsCount >= MaxActiveSessionsCount)
                throw new InvalidOperationException("Max number of active sessions reached.");

            var session = new UserSession
            {
                Token = Guid.NewGuid(),
                State = UserSessionState.Active,
                CreationDate = DateTime.UtcNow,
                UserId = user.Id
            };

            return _userSessionRepository.Add(session);
        }
    }
}
