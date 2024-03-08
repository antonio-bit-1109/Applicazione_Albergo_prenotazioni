using System.ComponentModel.DataAnnotations;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Servizio
    {
        [Key]
        public int IdServizio { get; set; }

        [Required]
        public string DescrizioneServizio { get; set; }

        [Required]
        public double CostoServizio { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
