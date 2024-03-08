using System.ComponentModel.DataAnnotations;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Dipendente
    {
        [Key]
        public int IdDipendente { get; set; }
        [Required]
        public string NomeUtente { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
