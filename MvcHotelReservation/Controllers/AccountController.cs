using Microsoft.AspNetCore.Mvc;
using MvcHotelReservation.Models;
using MvcHotelReservation.Data;
namespace MvcHotelReservation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        //[HttpGet]
        /*public ActionResult<IEnumerable<Utilisateur>> GetUtilisateurs()
        {
            return Ok(_context.Utilisateurs.ToList());
        }*/
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilisateur
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateurs.FindAsync());
        }*/
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
        /*
        [HttpPost("username")]
        public IActionResult Login(string username)
        {
            // Example login logic
            if (!string.IsNullOrEmpty(username))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
*/
        [HttpPost("email,password")]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = _context.Utilisateurs
                    .FirstOrDefault(u => u.email == email && u.motDePasse == password);
               

                if (utilisateur != null)
                {
                    HttpContext.Session.SetInt32("idClient",utilisateur.idUtilisateur);
                    HttpContext.Session.SetString("connected", "yes");
                    HttpContext.Session.SetString("email", utilisateur.email);
                    HttpContext.Session.SetString("FullName", utilisateur.nom + " " + utilisateur.prenom);
                    HttpContext.Session.SetString("imagePath", utilisateur.imagePath);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("nom,prenom,email,motDePasse,telephone")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.imagePath="/images/1721270428129.jpeg";
                utilisateur.dateInscription = DateTime.Now;
                
                _context.Utilisateurs.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/images/{uniqueFileName}";
        }
            
            
            
            
            
            
            
            
               /* if (ModelState.IsValid)
                {
                    // Check if the email is already in the database
                    if (_context.Utilisateurs.Any(u => u.email ==email))
                    {
                        ModelState.AddModelError("", "Email already exists.");
                        return View(); // Return to the form with an error message
                    }

                    // Create a new Utilisateur object
                    var utilisateur = new Utilisateur(nom, prenom, email, password, phone);
                    
                    // Save the user to the database
                    _context.Utilisateurs.Add(utilisateur);
                    _context.SaveChanges();

                    // Redirect to the login page or home page after successful registration
                    return RedirectToAction("Index", "Home");
                }

                // If model validation fails, return the same view with validation errors
                return RedirectToAction("Index", "Home");*/
        
        
        
    
        
        // Optional: Logout Action



        public IActionResult Index()
        {
            HttpContext.Session.GetString("username");
            return View();
        }

        public IActionResult Profil()
        {
            return View();
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
    
    
}