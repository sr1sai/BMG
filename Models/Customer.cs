using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMG.Models
{
    public class Customer
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public List<Account>? accounts { get; set; }
        
        public Customer() { 
            List<Account> accounts = new List<Account>();
        }
        public Customer(string id,string name,string address,string phone,string email,List<Account> accounts)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone=phone;
            this.email = email;
            this.accounts = accounts;
        }
        
    }
}