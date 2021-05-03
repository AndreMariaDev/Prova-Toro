using App.Domain.Models; 
using App.Infra.Data.Context; 
using App.Infra.Data.Interfaces; 
 
namespace App.Infra.Data.Repositories 
{ 
    public class TransactionHistoryRepository : Repository<TransactionHistory>, ITransactionHistoryRepository
    {
        #region Constructor
        public TransactionHistoryRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion
    } 
} 
