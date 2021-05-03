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
        public ICollection<UserCredentials> UserCredentialsItens { get; set; }
        public ICollection<Assets> ListAssets { get; set; }
        public BankAccount BankAccount { get; set; }
    }

}
