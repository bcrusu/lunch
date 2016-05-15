using lunch.Domain.Security;

namespace lunch.BusinessLogic.Security
{
    public class ExternalUserDetails
    {
        public UserType UserType { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string PictureUrl { get; set; }
    }
}