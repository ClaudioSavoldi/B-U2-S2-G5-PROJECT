using System.ComponentModel.DataAnnotations;

namespace B_U2_S2_G5_PROJECT.Models.Dto
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public required string Password { get; set; }
    }
}
