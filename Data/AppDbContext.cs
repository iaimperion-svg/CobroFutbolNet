using Microsoft.EntityFrameworkCore;
using CobroFutbol.Web.Models;

namespace CobroFutbol.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(e => e.Monto).HasPrecision(18, 2);
                entity.Property(e => e.MontoReportado).HasPrecision(18, 2);
                entity.Property(e => e.ConfianzaIA).HasPrecision(5, 2);
            });
        }
    }
}