using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicazioneAlbergo_core_Entity.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Camera> Camere { get; set; }
        public DbSet<Pensione> Pensioni { get; set; }
        public DbSet<Servizio> Servizi { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Dipendente> Dipendenti { get; set; }
    }
}
