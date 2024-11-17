using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMG.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BMG.Controllers;

public class LoginController: Controller {
    private readonly ILogger<LoginController> _logger;

    public List<Customer> customers = new List<Customer>();
    public List<string> usernames = new List<string>();
    public Customer currentUser = null;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login() {
        return View();

    }

    [HttpPost]
    public IActionResult Login_form(string username, string password)
    {

        Customer user = customers.FirstOrDefault(c => c.username == username);
        if (user != null && user.password == password)
        {
            currentUser = user;
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Invalid login attempt.");
        return View("Login");
    }

    [HttpPost]
    public IActionResult Register_form(Customer customer)
    {
        if (usernames.Contains(customer.username))
        {
            ModelState.AddModelError("", "Username already exists.");
            return View("Login");
        }

        if (ModelState.IsValid)
        {

            customers.Add(customer);
            usernames.Add(customer.username);
            Console.WriteLine(customers[customers.Count - 1].name);


            return RedirectToAction("Login");
        }

        return View("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}