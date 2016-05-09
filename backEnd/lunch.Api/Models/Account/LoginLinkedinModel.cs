using System.ComponentModel.DataAnnotations;

namespace lunch.Api.Models.Account
{
    public class LoginLinkedinModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string RedirectUri { get; set; }

        [Required]
        public string State { get; set; }
    }
}