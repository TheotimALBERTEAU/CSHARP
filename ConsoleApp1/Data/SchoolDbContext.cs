using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.Data
{
    public class SchoolDbContext : DbContext
    {
        // Tables générées automatiquement par EF Core
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Detail> Details { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Classe>().ToTable("classe");
            modelBuilder.Entity<Profil>().ToTable("profil");
            modelBuilder.Entity<Detail>().ToTable("detail");

            modelBuilder.Entity<Profil>()
                .HasOne(p => p.Classe)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.ClassId)
                .HasConstraintName("FK_profil_classe")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            modelBuilder.Entity<Profil>()
                .HasMany(p => p.AddressDetails) 
                .WithMany(d => d.Persons) 
                .UsingEntity(j => j.ToTable("ProfilDetails"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Charger la configuration manuellement
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    @"C:\Users\Théotim\RiderProjects\ConsoleApp1\ConsoleApp1\appsettings.json")
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            }
            // Chaîne de connexion PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProjetTest;Username=postgres;Password=P0stgresql*th");
        }


    }
}