using System;
using System.Collections.Generic;
using BMG.Models;
namespace BMG.Services
{
    public class UserService
    {
        public List<Customer> customers { get; set; } = new List<Customer>();
        public List<string> usernames { get; set; } = new List<string>();
        public Customer? currentUser { get; set; } // Make currentUser nullable
    }
}
