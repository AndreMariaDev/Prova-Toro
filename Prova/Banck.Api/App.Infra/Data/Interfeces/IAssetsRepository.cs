using App.Domain.Models;
using App.Infra.Data.Interfeces;
using App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IAssetsRepository : IRepository<Assets>
    {
        Task<List<Patrimony>> GetPatrimonyByUser(Guid codeUser);
        Task<Boolean> AddAsset(Assets entity);
        Task<List<TopFiveTraded>> GetTopFiveTradedInSeventeDays();
    } 
} 
