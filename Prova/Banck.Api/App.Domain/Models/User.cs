using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class User: DomainEntity
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public enumTypeUser TypeUser { get; set; }
        public ICollection<UserCredentials> UserCredentialsItens { get; set; }
        public ICollection<Assets> ListAssets { get; set; }
        public BankAccount BankAccount { get; set; }
    }

    public enum enumTypeUser {
        Undefined = 0,
        Accountant = 1,
        Admin = 2
    }

}
