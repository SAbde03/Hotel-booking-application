namespace MvcHotelReservation.Models;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public int IdReservation { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string Statut { get; set; } = "Confirmed"; // "confirmée", "en attente", "annulée"
    public int MontantTotal { get; set; }
    public DateTime DateReservation { get; set; } = DateTime.Now;

    public Utilisateur Utilisateur { get; set; }
    [Key]
    public Chambre Chambre { get; set; }

    public Reservation(){}
    public Reservation(DateTime dateDebut, DateTime dateFin, int? montant, Utilisateur utilisateur, Chambre chambre)
    {
        DateDebut = dateDebut;
        DateFin = dateFin;
        MontantTotal = montant ?? 0;
        Utilisateur = utilisateur;
        Chambre = chambre;
    }
    
}