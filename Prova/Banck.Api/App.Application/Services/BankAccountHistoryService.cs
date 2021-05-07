using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Services 
{ 
    public class BankAccountHistoryService : BaseService<BankAccountHistory>, IBankAccountHistoryService 
    { 
        private readonly IBankAccountHistoryRepository _BankAccountHistoryRepository;
 
        public BankAccountHistoryService(IBankAccountHistoryRepository BankAccountHistoryRepository) :base(BankAccountHistoryRepository) 
        { 
            _BankAccountHistoryRepository = BankAccountHistoryRepository; 
        }

        public async Task<List<BankAccountHistory>> GetBankAccountHistoryByUser(Guid codeUser) 
        {
            return await _BankAccountHistoryRepository.GetBankAccountHistoryByUser(codeUser);
        }
    } 
} 
