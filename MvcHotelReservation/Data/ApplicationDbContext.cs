using Microsoft.EntityFrameworkCore;
using MvcHotelReservation.Models;
namespace MvcHotelReservation.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Chambre>()
            .HasKey(c => c.IdChambre);
        modelBuilder.Entity<Paiement>()
            .HasKey(c => c.IdPaiement);
    }

    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Chambre> Chambres { get; set; }
    public DbSet<Paiement> Paiements { get; set; }
}