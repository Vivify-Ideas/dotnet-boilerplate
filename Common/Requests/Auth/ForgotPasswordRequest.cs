
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.Requests.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "E-mail is not valid.")]
        public string Email { get; set; }
    }
}
