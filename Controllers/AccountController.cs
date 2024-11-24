using System;
using BMG.Services;
using BMG.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BMG.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserService _userService;

    public AccountController(ILogger<AccountController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult Create(Account account)
    {
        //Console.WriteLine("CreateAccount called with type: " + account.type + ", roi: " + account.roi + ", DOC: " + account.doc, + ", balance: " + account.balance);

        if (_userService.currentUser != null)
        {
            Console.WriteLine("Current user found: " + _userService.currentUser.username);

            if (_userService.currentUser.accounts == null)
            {
                _userService.currentUser.accounts = new List<Account>();
            }

            account.id = Guid.NewGuid().ToString();

            _userService.currentUser.accounts.Add(account);
            Console.WriteLine("Account created with id: " + account.id);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            Console.WriteLine("No current user found.");
            ModelState.AddModelError("", "User Not Found.");
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult Delete()
    {
        if (_userService.currentUser != null)
        {
            Account? account = _userService.currentAccount;
            if (account != null)
            {
                if (_userService.currentUser.accounts != null)
                {
                    _userService.currentUser.accounts.Remove(account);
                }
                _userService.currentAccount = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Account Not Found.");
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            Console.WriteLine("No current user found.");
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult EditAccount()
    {
        if (_userService.currentUser != null)
        {
            return View("~/Views/Account/EditAccount.cshtml", _userService.currentAccount);
        }
        else
        {
            Console.WriteLine("No current user found.");
            return RedirectToAction("Login", "Login");
        }
    }

    public IActionResult Edit(Account account)
    {
        if (_userService.currentUser != null)
        {
            Console.WriteLine("Current user found: " + _userService.currentUser.username);
            Account? oldAccount = null;
            if (_userService.currentUser.accounts != null && _userService.currentAccount != null)
            {
                oldAccount = _userService.currentUser.accounts.Find(a => a.id == _userService.currentAccount.id);
            }
            if (oldAccount != null)
            {
                oldAccount.type = account.type;
                oldAccount.roi = account.roi;
                oldAccount.balance = account.balance;
                _userService.currentAccount = account;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Account Not Found.");
            }
            return RedirectToAction("Index", "Home");
        }
        else
        {
            Console.WriteLine("No current user found.");
            return RedirectToAction("Login", "Login");
        }
    }
    
    public IActionResult Transaction()
    {
        if (_userService.currentUser != null)
        {
            return View("~/Views/Transaction/Transaction.cshtml",_userService.currentAccount);
        }
        else
        {
            Console.WriteLine("No current user found.");
            return RedirectToAction("Login", "Login");
        }
    }
}
