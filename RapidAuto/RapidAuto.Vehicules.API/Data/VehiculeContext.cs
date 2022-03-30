using Microsoft.EntityFrameworkCore;
using RapidAuto.Vehicules.API.Model;

namespace RapidAuto.Vehicules.API.Data
{
    public class VehiculeContext : DbContext
    {
        public VehiculeContext(DbContextOptions<VehiculeContext> options) : base(options)
        {
        }

        public DbSet<Vehicule> Vehicule { get; set; }
    }
}
