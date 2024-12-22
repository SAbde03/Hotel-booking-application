using Microsoft.AspNetCore.Mvc;
using MvcHotelReservation.Models;
namespace MvcHotelReservation.Controllers
{
    public class AccountController : Controller
    {
        private static readonly List<Utilisateur> Utilisateurs = new List<Utilisateur>();

        [HttpGet]
        public ActionResult<IEnumerable<Utilisateur>> GetUtilisateurs()
        {
            return Ok(Utilisateurs);
        }
        /* Display the Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }*/
        // Process login POST request
        [HttpGet("{email}/{password}")]
        public ActionResult <Utilisateur> Login(string email, string password)
        {
            
            var utilisateur = Utilisateurs.FirstOrDefault(u => u.email == email|| u.motDePasse == password);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return Ok(utilisateur);
        }
        
        public ActionResult Signin([FromBody] Utilisateur utilisateur)
        {
            if (utilisateur == null)
            {
                return BadRequest("Utilisateur cannot be null.");
            }

            utilisateur.idUtilisateur = Utilisateurs.Count > 0 ? Utilisateurs.Max(u => u.idUtilisateur) + 1 : 1;
            Utilisateurs.Add(utilisateur);
            return CreatedAtAction(nameof(Login), new { id = utilisateur.idUtilisateur }, utilisateur);
        }

        // Optional: Logout Action
        public IActionResult Logout()
        {
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}