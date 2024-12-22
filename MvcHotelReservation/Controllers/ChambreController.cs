using Microsoft.AspNetCore.Mvc;

namespace MvcHotelReservation.Controllers;
using Models;
[ApiController]
[Route("api/[controller]")]
public class ChambreController : Controller

   
    {
        private static readonly List<Chambre> Chambres = new List<Chambre>();

        [HttpGet]
        public ActionResult<IEnumerable<Chambre>> GetChambres()
        {
            return Ok(Chambres);
        }

        [HttpGet("{id}")]
        public ActionResult<Chambre> GetChambreByType(string type)
        {
            var chambre = Chambres.FirstOrDefault(c => c.TypeChambre == type);
            if (chambre == null)
            {
                return NotFound();
            }
            return Ok(chambre);
        }

        

        [HttpPut("{id}")]
        public ActionResult UpdateChambre(int id, [FromBody] Chambre updatedChambre)
        {
            var chambre = Chambres.FirstOrDefault(c => c.IdChambre == id);
            if (chambre == null)
            {
                return NotFound();
            }

            chambre.NumeroChambre = updatedChambre.NumeroChambre;
            chambre.TypeChambre = updatedChambre.TypeChambre;
            chambre.Capacite = updatedChambre.Capacite;
            chambre.PrixParNuit = updatedChambre.PrixParNuit;
            chambre.Description = updatedChambre.Description;
            chambre.Disponibilite = updatedChambre.Disponibilite;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteChambre(int id)
        {
            var chambre = Chambres.FirstOrDefault(c => c.IdChambre == id);
            if (chambre == null)
            {
                return NotFound();
            }

            Chambres.Remove(chambre);
            return NoContent();
        }
        [HttpPost("reserve/{id}")]
        public ActionResult ReserveChambre(int id)
        {
            var chambre = Chambres.FirstOrDefault(c => c.IdChambre == id);
            if (chambre == null)
            {
                return NotFound("Chambre not found.");
            }

            if (!chambre.Disponibilite)
            {
                return BadRequest("Chambre is not available.");
            }

            chambre.Disponibilite = false;
            return Ok("Chambre reserved successfully.");
        }
        
    }
    