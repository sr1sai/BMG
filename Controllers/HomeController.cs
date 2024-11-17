using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMG.Models;
using BMG.Services;

namespace BMG.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserService _userService;

    public HomeController(ILogger<HomeController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult Index()
    {
        if(_userService.currentUser!=null)
            return View();
        else
            return RedirectToAction("Login", "Login");
    }

    public IActionResult Privacy()
    {
        if (_userService.currentUser != null)
            return View();
        else
            return RedirectToAction("Login", "Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
