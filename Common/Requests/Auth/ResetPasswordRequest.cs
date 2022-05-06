
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.Requests.Auth
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
