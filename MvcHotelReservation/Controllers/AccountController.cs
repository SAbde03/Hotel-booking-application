using Microsoft.AspNetCore.Mvc;

namespace MvcHotelReservation.Controllers
{
    public class AccountController : Controller
    {
        // Display the Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        // Process login POST request
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simple authentication logic for demonstration only
            if (username == "admin" && password == "password")
            {
                // Authentication successful
                TempData["Message"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        // Optional: Logout Action
        public IActionResult Logout()
        {
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}