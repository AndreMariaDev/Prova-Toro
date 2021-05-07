using App.Domain.Models;
using App.Shared.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IAssetsService : IBaseService<Assets>
    {
        Task<bool> CreateCuston(Assets entity);
        Task<List<TopFiveTraded>> GetTopFiveTradedInSeventeDays();
    } 
} 
