using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMG.Models
{
    public class Loan
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
        public DateTime avail { get; set; }
        public int duration { get; set; }
        public DateTime deadline { get; set; }
        public double amount { get; set; }

    }
}