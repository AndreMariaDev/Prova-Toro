using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class BankAccountHistory : DomainEntity
    {
        public Guid BankAccountCode { get; set; }
        public DateTime MovingDate { get; set; }
        public enumTypeMoving TypeMoving { get; set; }
        public Decimal AmountMoved { get; set; }
        public BankAccount BankAccount { get; set; }
    }

    public enum enumTypeMoving
    {
        Undefined = 0,
        BankDraft = 1,
        BankDeposit = 2
    }
}
