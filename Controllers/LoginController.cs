using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMG.Models;
using BMG.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BMG.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly UserService _userService;

    public LoginController(ILogger<LoginController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login_form(string username, string password)
    {
        Customer? customer = _userService.customers.Find(c => c.username == username);

        if (customer != null)
        {
            if (customer.password == password)
            {
                _userService.currentUser = customer;
                Console.WriteLine("Login Success.");
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Incorrect password.");
            Console.WriteLine("Incorrect password.");
        }
        else
        {
            ModelState.AddModelError("", "User Not Found.");
            Console.WriteLine("User Not Found.");
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View("Login");
    }

    [HttpPost]
    public IActionResult Register_form(Customer customer)
    {
        if (_userService.usernames.Contains(customer.username ?? string.Empty))
        {
            ModelState.AddModelError("", "Username already exists.");
            Console.WriteLine("Username already exists.");
            return View("Login");
        }

        if (ModelState.IsValid)
        {
            customer.id = Guid.NewGuid().ToString(); // Generate unique ID for customer
            _userService.customers.Add(customer);
            _userService.usernames.Add(customer.username ?? string.Empty);
            Console.WriteLine("User Registered.");
            Console.WriteLine("ID: " + _userService.customers[_userService.customers.Count - 1].id);
            Console.WriteLine("Name: " + _userService.customers[_userService.customers.Count - 1].name);
            Console.WriteLine("Username: " + _userService.customers[_userService.customers.Count - 1].username);
            Console.WriteLine("Password: " + _userService.customers[_userService.customers.Count - 1].password);
            Console.WriteLine("Address: " + _userService.customers[_userService.customers.Count - 1].address);
            Console.WriteLine("Phone: " + _userService.customers[_userService.customers.Count - 1].phone);
            Console.WriteLine("Email: " + _userService.customers[_userService.customers.Count - 1].email);

            return RedirectToAction("Login");
        }

        return View("Login");
    }

    public IActionResult Logout()
    {
        _userService.currentUser = null;
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
