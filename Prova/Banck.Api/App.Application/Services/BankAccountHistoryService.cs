using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces; 
 
namespace App.Application.Services 
{ 
    public class BankAccountHistoryService : BaseService<BankAccountHistory>, IBankAccountHistoryService 
    { 
        private readonly IBankAccountHistoryRepository _BankAccountHistoryRepository;
 
        public BankAccountHistoryService(IBankAccountHistoryRepository BankAccountHistoryRepository) :base(BankAccountHistoryRepository) 
        { 
            _BankAccountHistoryRepository = BankAccountHistoryRepository; 
        } 
    } 
} 
