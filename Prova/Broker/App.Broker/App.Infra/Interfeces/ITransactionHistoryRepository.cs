using App.Domain.Models;
using App.Infra.Data.Interfeces;

namespace App.Infra.Data.Interfaces 
{ 
    public interface ITransactionHistoryRepository : IRepository<TransactionHistory>
    {
    } 
} 
