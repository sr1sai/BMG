using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMG.Models
{
    public class Transaction
    {
        public string? Id { get; set; }
        public string? type { get; set; }
        public string? sender { get; set; }
        public string? receiver { get; set; }
        public DateTime date { get; set; }
        public double amount { get; set; }
    }
}