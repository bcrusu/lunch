using System;
using lunch.BusinessLogic.Security;
using lunch.Domain.Security;
using lunch.Repositories.Security;

namespace lunch.BusinessLogic.Impl.Security
{
    internal class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public User CreateUser(ExternalUserDetails externalUserDetails)
        {
            if (externalUserDetails == null) throw new ArgumentNullException(nameof(externalUserDetails));

            var user = new User
            {
                Email = externalUserDetails.Email,
                FirstName = externalUserDetails.FirstName,
                LastName = externalUserDetails.LastName,
                DisplayName = externalUserDetails.DisplayName,
                PictureUrl = externalUserDetails.PictureUrl
            };

            return _userRepository.Add(user);
        }
    }
}
