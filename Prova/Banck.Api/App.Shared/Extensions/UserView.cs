using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Extensions
{
    public class UserView
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public enumCredentialsType CredentialsType { get; set; }
    }

    public enum enumCredentialsType
    {
        Umdefined = 0,
        CheckingAccount = 1,
        InvestmentAccount = 2
    }
}
