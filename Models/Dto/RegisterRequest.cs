using System.ComponentModel.DataAnnotations;

namespace B_U2_S2_G5_PROJECT.Models.Dto
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
