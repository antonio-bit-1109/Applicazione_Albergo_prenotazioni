using System.ComponentModel.DataAnnotations;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Pensione
    {
        [Key]
        public int IdPensione { get; set; }
        [Required]
        public string TipoPensione { get; set; }
        [Required]
        public double Prezzo { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
