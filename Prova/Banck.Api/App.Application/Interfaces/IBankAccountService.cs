using App.Domain.Models;
using App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IBankAccountService : IBaseService<BankAccount>
    {
        Task<BankBalance> GetAmountInAccount(Guid codeUser);
        Task<Boolean> GetExistBankAccount(String accountNumber, String branch);
        Task<List<BankAccount>> GetBankAccountByUser(Guid codeUser);
        Task<bool> RunTransaction(TransactionHistoryResponse entity);
    } 
} 
