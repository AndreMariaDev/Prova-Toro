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
        public enumTypeUser TypeUser { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public enumCredentialsType CredentialsType { get; set; }
    }

    public enum enumCredentialsType
    {
        Undefined = 0,
        CheckingAccount = 1,
        InvestmentAccount = 2
    }

    public enum enumTypeUser
    {
        Undefined = 0,
        Accountant = 1,
        Admin = 2
    }
}
