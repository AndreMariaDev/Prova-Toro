using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces; 
 
namespace App.Application.Services 
{ 
    public class TransactionHistoryService : BaseService<TransactionHistory>, ITransactionHistoryService 
    { 
        private readonly ITransactionHistoryRepository _TransactionHistoryRepository;
 
        public TransactionHistoryService(ITransactionHistoryRepository TransactionHistoryRepository) :base(TransactionHistoryRepository) 
        { 
            _TransactionHistoryRepository = TransactionHistoryRepository; 
        } 
    } 
} 
