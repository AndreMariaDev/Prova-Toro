using App.Domain.Models;
using App.Infra.Data.Interfeces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        Task<Boolean> GetExistBankAccount(String accountNumber, String branch);
        Task<List<BankAccount>> GetBankAccountByUser(Guid codeUser);
        Task<Boolean> RunTransaction(App.Shared.TransactionHistory entity);
    } 
} 
