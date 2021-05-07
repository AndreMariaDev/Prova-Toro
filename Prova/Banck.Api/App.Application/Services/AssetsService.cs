using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces;
using App.Shared.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Services 
{ 
    public class AssetsService : BaseService<Assets>, IAssetsService 
    { 
        private readonly IAssetsRepository _AssetsRepository;
        private readonly IBankAccountRepository _BankAccountRepository;

        public AssetsService(IAssetsRepository AssetsRepository, IBankAccountRepository BankAccountRepository) :base(AssetsRepository) 
        { 
            _AssetsRepository = AssetsRepository;
            _BankAccountRepository = BankAccountRepository;
        }

        public async Task<bool> CreateCuston(Assets entity)
        {
            var resultAccount = await this._BankAccountRepository.GetAmountInAccount(entity.UserCode);
            if (null != resultAccount) 
            {
                var sum = resultAccount.Amount - entity.Value;
                if (sum < 0) 
                {
                    return false;
                }
            }
            return await _AssetsRepository.AddAsset(entity);
        }

        public async Task<List<TopFiveTraded>> GetTopFiveTradedInSeventeDays() 
        {
            return await _AssetsRepository.GetTopFiveTradedInSeventeDays();
        }
    } 
} 
