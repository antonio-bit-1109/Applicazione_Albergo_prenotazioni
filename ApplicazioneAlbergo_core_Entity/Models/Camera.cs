using System.ComponentModel.DataAnnotations;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Camera
    {
        [Key]
        public int IdCamera { get; set; }

        [Required]
        public int NumeroCamera { get; set; }

        [Required]
        public string TipoCamera { get; set; }

        [Required]
        [Display(Name = "Prezzo Giornaliero")]
        public double Prezzo { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
