using App.Domain.Models;
using App.Infra.Data.Interfeces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IBankAccountHistoryRepository : IRepository<BankAccountHistory>
    {
        Task<List<BankAccountHistory>> GetBankAccountHistoryByUser(Guid codeUser);
    } 
} 
