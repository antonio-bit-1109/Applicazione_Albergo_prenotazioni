using System.ComponentModel.DataAnnotations;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }

        [Required]
        public string CodiceFiscale { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Cellulare { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
