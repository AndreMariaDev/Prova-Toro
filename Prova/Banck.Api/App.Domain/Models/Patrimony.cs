using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Patrimony
    {
        public String AccountNumber { get; set; }
        public String Branch { get; set; }
        public Decimal Balance { get; set; }
        public List<Assets> ListAssets { get; set; }
        public Decimal SummarizedEquity { get; set; }
        public Guid UserCode { get; set; }
        public String Name { get; set; }
    }
}
