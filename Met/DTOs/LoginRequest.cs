using System.ComponentModel.DataAnnotations;

namespace Met.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
