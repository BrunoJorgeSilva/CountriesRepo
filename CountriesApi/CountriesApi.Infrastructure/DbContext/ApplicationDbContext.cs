using Microsoft.EntityFrameworkCore;
using CountriesApi.Domain.Entities;

namespace CountriesApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
                .HasMaxLength(255)  
                .IsRequired();      

            modelBuilder.Entity<Country>()
                .Property(c => c.Capital)
                .HasMaxLength(255)
                .IsRequired(false); 

            modelBuilder.Entity<Country>()
                .Property(c => c.Region)
                .HasMaxLength(100)
                .IsRequired(false);

            modelBuilder.Entity<Country>()
                .Property(c => c.SubRegion)
                .HasMaxLength(100)
                .IsRequired(false);

            modelBuilder.Entity<Country>()
                .Property(c => c.Population)
                .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(c => c.Latitude)
                .HasColumnType("decimal(18,10)") // Define a precisão e escala para o tipo decimal
                .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(c => c.Longitude)
                .HasColumnType("decimal(18,10)")
                .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(c => c.Borders)
                .HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Country>()
                .Property(c => c.Timezones)
                .HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<Country>()
                .Property(c => c.Currency)
                .HasMaxLength(100)
                .IsRequired(false);

            modelBuilder.Entity<Country>()
                .Property(c => c.Languages)
                .HasMaxLength(500)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
