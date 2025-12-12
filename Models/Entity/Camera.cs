using B_U2_S2_G5_PROJECT.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Camera
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Il numero della camera è obbligatorio")]
    [StringLength(10)]
    public string Numero { get; set; }

    [Required(ErrorMessage = "Il tipo di camera è obbligatorio")]
    [StringLength(50)]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "Il prezzo è obbligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore o uguale a 0")]
    public decimal Prezzo { get; set; }

    public ICollection<Prenotazione>? Prenotazioni { get; set; }
}
