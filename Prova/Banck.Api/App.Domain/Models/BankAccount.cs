using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class BankAccount: DomainEntity
    {
        public String AccountNumber { get; set; }
        public String Branch { get; set; }
        public String Document { get; set; }
        public enumTypeAccount TypeAccount { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Limit { get; set; }
        public bool HasLimit { get; set; }
        public Guid UserCode { get; set; }
        public User User { get; set; }
        public ICollection<BankAccountHistory> ListBankAccountHistories { get; set; }
    }

    public enum enumTypeAccount
    {
        Umdefined = 0,
        LegalPerson = 1,
        PhysicalPerson = 2
    }
}
