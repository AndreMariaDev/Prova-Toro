using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared
{
    public class Target
    {
        public string bank { get; set; }
        public string branch { get; set; }
        public string account { get; set; }
    }

    public class Origin
    {
        public string bank { get; set; }
        public string branch { get; set; }
        public string cpf { get; set; }
    }
    public class TransactionHistory 
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
