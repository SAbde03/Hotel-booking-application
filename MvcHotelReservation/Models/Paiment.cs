namespace MvcHotelReservation.Models;
using System.ComponentModel.DataAnnotations;
public class Paiement
{
    [Key]
    public int IdPaiement { get; set; }
    public int? IdReservation { get; set; }
    public decimal? Montant { get; set; }
    public DateTime DatePaiement { get; set; } = DateTime.Now;
    public string MethodePaiement { get; set; } // "carte", "paypal", "virement", "espèces"
    public string Statut { get; set; } = "en attente"; // "réussi", "échoué", "en attente"

    public Reservation Reservation { get; set; }
}