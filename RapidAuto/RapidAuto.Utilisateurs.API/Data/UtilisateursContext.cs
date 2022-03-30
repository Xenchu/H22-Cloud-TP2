using Microsoft.EntityFrameworkCore;
using RapidAuto.Utilisateurs.API.Model;

namespace RapidAuto.Utilisateurs.API.Data
{
    public class UtilisateursContext : DbContext
    {
        public UtilisateursContext(DbContextOptions<UtilisateursContext> options) : base(options) { }

        public DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>().ToTable("Utilisateur");
        }
    }
}
