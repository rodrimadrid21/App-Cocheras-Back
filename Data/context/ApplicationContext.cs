using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cochera> Cocheras { get; set; }
        public DbSet<Estacionamiento> Estacionamientos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Estacionamiento y Cochera
            modelBuilder.Entity<Estacionamiento>()
                .HasOne(e => e.Cochera) // Estacionamiento tiene una Cochera
                .WithMany() // Cochera no tiene una lista de Estacionamientos asociados
                .HasForeignKey(e => e.IdCochera); // Usa IdCochera como llave foránea

            base.OnModelCreating(modelBuilder); // Llama al método base
        }
    }
}
