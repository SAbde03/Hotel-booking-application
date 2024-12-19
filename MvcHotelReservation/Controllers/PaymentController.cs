namespace MvcHotelReservation.Controllers;


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcHotelReservation.Models;


public class PaymentController : Controller
{
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Payment()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
