using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces; 
 
namespace App.Application.Services 
{ 
    public class TradedService : BaseService<Traded>, ITradedService 
    { 
        private readonly ITradedRepository _TradedRepository;
 
        public TradedService(ITradedRepository TradedRepository) :base(TradedRepository) 
        { 
            _TradedRepository = TradedRepository; 
        } 
    } 
} 
