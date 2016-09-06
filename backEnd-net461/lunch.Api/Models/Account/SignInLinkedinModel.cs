using System.ComponentModel.DataAnnotations;

namespace lunch.Api.Models.Account
{
    public class SignInLinkedinModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string RedirectUri { get; set; }
    }
}