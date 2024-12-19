using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcHotelReservation.Models;
namespace MvcHotelReservation.Controllers;

public class VisualisationController : Controller
{
    private readonly ILogger<VisualisationController> _logger;

    public VisualisationController(ILogger<VisualisationController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Visualisation()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
