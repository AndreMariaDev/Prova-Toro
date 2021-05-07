using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IBankAccountHistoryService : IBaseService<BankAccountHistory>
    {
        Task<List<BankAccountHistory>> GetBankAccountHistoryByUser(Guid codeUser);
    } 
} 
