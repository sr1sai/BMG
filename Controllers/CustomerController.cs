using System;
using BMG.Services;
using BMG.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BMG.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly UserService _userService;

        public CustomerController(ILogger<CustomerController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Edit(Customer customer)
        {
            Console.WriteLine("Edit Customer called with id: " + customer.id + ", name: " + customer.name + ", address: " + customer.address + ", phone: " + customer.phone + ", email: " + customer.email);
            if (_userService.currentUser != null)
            {
                Customer? old = _userService.customers.Find(c => c.id == customer.id);

                if (old != null)
                {
                    Console.WriteLine("Old Customer:");
                    Console.WriteLine($"{"ID",-10} {"Name",-20} {"Address",-30} {"Phone",-15} {"Email",-25}");
                    Console.WriteLine($"{old.id,-10} {old.name,-20} {old.address,-30} {old.phone,-15} {old.email,-25}");

                    Console.WriteLine("New Customer:");
                    Console.WriteLine($"{"ID",-10} {"Name",-20} {"Address",-30} {"Phone",-15} {"Email",-25}");
                    Console.WriteLine($"{customer.id,-10} {customer.name,-20} {customer.address,-30} {customer.phone,-15} {customer.email,-25}");
                    
                    customer.accounts = old.accounts;
                    _userService.customers.Remove(old);
                    _userService.customers.Add(customer);
                    _userService.currentUser = customer;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found.");
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult Delete()
        {
            if (_userService.currentUser != null && _userService.currentUser.username!=null)
            {
                _userService.usernames.Remove(_userService.currentUser.username);
                _userService.customers.Remove(_userService.currentUser);
                _userService.currentUser = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found.");
                return RedirectToAction("Login", "Login");
            }
        }
    }
}