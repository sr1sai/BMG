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
        if (_userService.currentUser != null)
        {
            return View(_userService.currentUser);
        }
        else
        {
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult Profile()
    {
        if (_userService.currentUser != null)
        {
            return View("~/Views/Customer/Profile.cshtml", _userService.currentUser);
        }
        else
        {
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult NewAccount()
    {
        if (_userService.currentUser != null)
        {
            return View("~/Views/Account/NewAccount.cshtml");
        }
        else
        {
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult Select(string id)
    {
        Console.WriteLine("Selecting account with id: " + id);
        if (_userService.currentUser != null)
        {
            var accounts = _userService.currentUser.accounts;
            if (accounts != null)
            {
                Account? account = accounts.Find(a => a.id == id);
                if (account != null)
                {
                    Console.WriteLine("Account found: " + account.id);
                    _userService.currentAccount = account;
                    return View("~/Views/Account/Account.cshtml", account);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Login", "Login");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
