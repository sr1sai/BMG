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
            if (_userService.currentUser != null)
            {
                Customer? old = _userService.customers.Find(c => c.id == customer.id);
                if (old != null)
                {
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