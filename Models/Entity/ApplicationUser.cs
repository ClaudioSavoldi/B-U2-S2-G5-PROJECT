using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace B_U2_S2_G5_PROJECT.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il Cognome è obbligatorio")]
        public string Surname { get; set; }


    }
}