using System.ComponentModel.DataAnnotations;

namespace lunch.Domain.Security
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string PictureUrl { get; set; }
    }
}
