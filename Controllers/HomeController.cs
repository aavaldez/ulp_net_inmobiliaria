using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public ActionResult Index()
    {
        return View();
    }

    [Authorize]
    public ActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public ActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public ActionResult Restringido()
    {
        return View();
    }
}
