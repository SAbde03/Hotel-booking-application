using System.ComponentModel.DataAnnotations;

namespace MvcHotelReservation.Models;

public class Chambre
{
    

    [Key]
    public int IdChambre { get; set; }
    public string NumeroChambre { get; set; }
    public string TypeChambre { get; set; } // "simple", "double", "suite", "Domestic", "standard", "deluxe"
    public int? Capacite { get; set; }
    public int? PrixParNuit { get; set; }
    public string Description { get; set; }
    public bool Disponibilite { get; set; } = true;
    
    public string ImagePath { get; set; } = null;
}
