using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lunch.Domain.Security
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(254)]
        [Required]
        [Index]
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

        [MaxLength(2048)]
        [Required]
        public string PictureUrl { get; set; }
    }
}
