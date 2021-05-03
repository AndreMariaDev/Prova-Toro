using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Assets : DomainEntity
    {
        public String AddedStockMarket { get; set; }
        public int Amount { get; set; }
        public Decimal Value { get; set; }
        public Guid UserCode { get; set; }
        public User User { get; set; }
    }
}
