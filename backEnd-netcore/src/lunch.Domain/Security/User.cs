using System.ComponentModel.DataAnnotations;

namespace lunch.Domain.Security
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string ExternalId { get; set; }

        [Required]
        public UserType Type { get; set; }

        [MaxLength(254)]
        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(200)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(1024)]
        [Required]
        public string Description { get; set; }

        [MaxLength(2048)]
        public string PictureUrl { get; set; }
    }
}
