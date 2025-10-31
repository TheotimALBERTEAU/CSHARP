using ConsoleApp1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.Data;
public class SchoolDbContext : DbContext
{
    // --- Tables principales ---
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Profil> Profil { get; set; }
    public DbSet<Detail> Details { get; set; }

    // --- Constructeur ---
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }
    
    // Constructeur vide pour EF CLI
    public SchoolDbContext() { }

    // --- Configuration des relations ---
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relation Classe -> Person (1..n)
        modelBuilder.Entity<Profil>()
            .HasOne(p => p.Classe)
            .WithMany(c => c.Persons)
            .HasForeignKey(p => p.IdClasse);
    }

    // --- Configuration de la connexion ---
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Charger la configuration manuellement
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\Théotim\RiderProjects\ConsoleApp1\ConsoleApp1\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}