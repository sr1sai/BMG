using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMG.Models;
using BMG.Services;
using System.Collections.Generic;
using System.Linq;
using System;


namespace BMG.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly UserService _userService;

        public TransactionController(ILogger<TransactionController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Withdraw(Transaction transaction)
        {
            if (_userService.currentUser != null)
            {
                if (_userService.currentAccount != null)
                {
                    if (transaction != null)
                    {
                        if (_userService.currentAccount.balance >= transaction.amount)
                        {
                            _userService.currentAccount.balance -= transaction.amount;
                            _userService.currentAccount.transactions.Add(transaction);
                            Console.WriteLine("Withdrawal successful.");
                            return View("~/Views/Account/Account.cshtml",_userService.currentAccount);
                        }
                        ModelState.AddModelError("", "Insufficient funds.");
                        Console.WriteLine("Insufficient funds.");
                        return View("~/Views/Transaction/Transaction.cshtml",_userService.currentAccount);
                    }
                    ModelState.AddModelError("", "Empty Transaction");
                    Console.WriteLine("Empty Transaction.");
                    return RedirectToAction("Select", "Home");
                }
                ModelState.AddModelError("", "No account selected.");
                Console.WriteLine("No account selected.");
                return RedirectToAction("Select", "Home");
            }
            ModelState.AddModelError("", "Not logged in.");
            Console.WriteLine("Not logged in.");
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Deposit(Transaction transaction)
        {
            if (_userService.currentUser != null)
            {
                if (_userService.currentAccount != null)
                {
                    if (transaction != null)
                    {
                        _userService.currentAccount.balance += transaction.amount;
                        _userService.currentAccount.transactions.Add(transaction);
                        Console.WriteLine("Deposit successful.");
                        return View("~/Views/Account/Account.cshtml",_userService.currentAccount);

                    }
                    ModelState.AddModelError("", "No account selected.");
                    Console.WriteLine("No account selected.");
                    return RedirectToAction("Select", "Home");
                }
            }
            ModelState.AddModelError("", "Not logged in.");
            Console.WriteLine("Not logged in.");
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Transfer(Transaction transaction)
        {
            if (_userService.currentUser != null)
            {
                if (_userService.currentAccount != null)
                {
                    if (transaction != null)
                    {
                        if (transaction.receiver != null)
                        {
                            Account? receiverAccount=null;
                            foreach (Customer customer in _userService.customers)
                            {
                                receiverAccount = customer.accounts.Find(a => a.id == transaction.receiver);
                                if (receiverAccount != null)
                                {
                                    break;
                                }
                            }
                            
                            if (receiverAccount != null)
                            {
                                if (_userService.currentAccount.balance >= transaction.amount)
                                {
                                    _userService.currentAccount.balance -= transaction.amount;
                                    receiverAccount.balance += transaction.amount;
                                    _userService.currentAccount.transactions.Add(transaction);
                                    receiverAccount.transactions.Add(transaction);
                                    Console.WriteLine("Transfer successful.");
                                    return View("~/Views/Account/Account.cshtml",_userService.currentAccount);
                                }
                                ModelState.AddModelError("", "Insufficient funds.");
                                Console.WriteLine("Insufficient funds.");
                                return View("~/Views/Transaction/Transaction.cshtml",_userService.currentAccount);
                            }
                            ModelState.AddModelError("", "Receiver account not found.");
                            Console.WriteLine("Receiver account not found.");
                            return View("~/Views/Transaction/Transaction.cshtml",_userService.currentAccount);
                        }
                    }
                    ModelState.AddModelError("", "Empty Transaction");
                    Console.WriteLine("Empty Transaction.");
                    return RedirectToAction("Select", "Home");
                }
                ModelState.AddModelError("", "No account selected.");
                Console.WriteLine("No account selected.");
                return RedirectToAction("Select", "Home");
            }
            ModelState.AddModelError("", "Not logged in.");
            Console.WriteLine("Not logged in.");
            return RedirectToAction("Login", "Login");
        }
    }
}