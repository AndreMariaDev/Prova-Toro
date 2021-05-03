using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Extensions
{
    public class BankBalance
    {
        public String AccountNumber { get; set; }
        public String Branch { get; set; }
        public String TypeAccount { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Limit { get; set; }
    }
}
