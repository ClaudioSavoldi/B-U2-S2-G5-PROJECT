using System;
using System.ComponentModel.DataAnnotations;


public class Prenotazione
{
    [Key]
    public Guid Id { get; set; }


    [Required]
    public Guid ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    // FK Camera
    [Required]
    public Guid CameraId { get; set; }
    public Camera? Camera { get; set; }

    [Required(ErrorMessage = "La data di inizio è obbligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Data Inizio")]
    public DateTime DataInizio { get; set; }

    [Required(ErrorMessage = "La data di fine è obbligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Data Fine")]
    public DateTime DataFine { get; set; }

    [Required]
    public bool Stato { get; set; }
}
