using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicazioneAlbergo_core_Entity.Models
{
    public class Prenotazione
    {
        [Key]
        public int IdPrenotazione { get; set; }

        [Required]
        public DateTime DataInizioPrenotazione { get; set; }

        [Required]
        public DateTime DataFinePrenotazione { get; set; }

        [Required]
        public int Acconto { get; set; }


        [Required]
        [ForeignKey("Cliente")]
        [Display(Name = "Nome cliente che effettua la prenotazione")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Camera")]
        [Display(Name = "Camera assegnata numero:")]
        public int IdCamera { get; set; }

        [Required]
        [ForeignKey("Pensione")]
        [Display(Name = "Tipo di Soggiorno Scelto:")]
        public int IdPensione { get; set; }

        [Required]
        [ForeignKey("Servizio")]
        [Display(Name = "Servizio Aggiuntivo:")]
        public int IdServizio { get; set; }


        // prop navigazione ogni prenotazione ha un riferimento ad un singolo cliente, camera, servizio, pensione 
        public Cliente Cliente { get; set; }
        public Camera Camera { get; set; }
        public Servizio Servizio { get; set; }
        public Pensione Pensione { get; set; }
    }
}
