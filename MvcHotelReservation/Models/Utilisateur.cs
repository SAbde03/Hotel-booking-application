namespace MvcHotelReservation.Models;
using System.ComponentModel.DataAnnotations;

public class Utilisateur
{
    [Key]
    public int idUtilisateur { get; set; }
    public string nom { get; set; }
    public string prenom { get; set; }
    public string email { get; set; }
    public string motDePasse { get; set; }
    public string telephone { get; set; }
    public DateTime dateInscription { get; set; } = DateTime.Now;
}