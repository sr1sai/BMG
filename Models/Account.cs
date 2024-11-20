using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMG.Models
{
    public class Account
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public double roi { get; set; }
        public DateTime doc { get; set; }
        public double balance { get; set; }
        public List<Transaction>? transactions { get; set; }
        public Account() {
            this.transactions=new List<Transaction>();
        }
        public Account(string id,string type,double roi,double balance,List<Transaction> transactions) {
            this.id = id;
            this.type = type;
            this.roi = roi;
            this.balance = balance;
            this.transactions = transactions;
        }
    }
}