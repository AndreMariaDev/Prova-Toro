using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Traded : DomainEntity
    {
        public String AddedStockMarket { get; set; }
        public Decimal Value { get; set; }
    }
}
