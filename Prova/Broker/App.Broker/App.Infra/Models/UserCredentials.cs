using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class UserCredentials : DomainEntity
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public enumCredentialsType CredentialsType { get; set; }
        public Guid UserCode { get; set; }
        public virtual User User { get; set; }
    }

    public enum enumCredentialsType 
    {
        Umdefined = 0,
        CheckingAccount = 1,
        InvestmentAccount =2
    }
}
