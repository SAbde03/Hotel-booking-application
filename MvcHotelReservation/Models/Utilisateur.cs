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
    
    public String imagePath { get; set; }
    public DateTime dateInscription { get; set; } = DateTime.Now;
    
    public Utilisateur() { }
    
    public Utilisateur(string nom, string prenom, string email, string motDePasse, string telephone)
    {
        this.nom = nom;
        this.prenom = prenom;
        this.email = email;
        this.motDePasse = motDePasse;
        this.telephone = telephone;
        this.dateInscription = DateTime.Now;
        this.imagePath = null;
    }
}