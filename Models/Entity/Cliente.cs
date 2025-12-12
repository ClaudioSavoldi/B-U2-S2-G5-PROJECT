using B_U2_S2_G5_PROJECT.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Il nome è obbligatorio")]
    [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Il cognome è obbligatorio")]
    [StringLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri")]
    public string Cognome { get; set; }

    [Required(ErrorMessage = "L'email è obbligatoria")]
    [EmailAddress(ErrorMessage = "Formato email non valido")]
    [StringLength(100)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Il telefono è obbligatorio")]
    [Phone(ErrorMessage = "Formato numero di telefono non valido")]
    [StringLength(20)]
    public string Telefono { get; set; }

    public ICollection<Prenotazione>? Prenotazioni { get; set; }
}
