using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class TransactionHistory: DomainEntity
    {
        public string Event { get; set; }
        public string TargetBank { get; set; }
        public string TargetBranch { get; set; }
        public string TargetAccount { get; set; }
        public string OriginBank { get; set; }
        public string OriginBranch { get; set; }
        public string OriginDocument { get; set; }
        public int Amount { get; set; }
    }
}
