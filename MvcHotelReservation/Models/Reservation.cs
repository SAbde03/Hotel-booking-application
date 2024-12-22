namespace MvcHotelReservation.Models;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public int IdReservation { get; set; }
    public int? IdUtilisateur { get; set; }
    public int? IdChambre { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string Statut { get; set; } = "en attente"; // "confirmée", "en attente", "annulée"
    public decimal? MontantTotal { get; set; }
    public DateTime DateReservation { get; set; } = DateTime.Now;

    public Utilisateur Utilisateur { get; set; }
    public Chambre Chambre { get; set; }
}