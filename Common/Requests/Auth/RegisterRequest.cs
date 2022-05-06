
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.Requests.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
