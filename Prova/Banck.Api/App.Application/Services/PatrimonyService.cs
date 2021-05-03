using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Services 
{ 
    public class PatrimonyService: IPatrimonyService 
    { 
        private readonly IAssetsRepository _AssetsRepository;
 
        public PatrimonyService(IAssetsRepository AssetsRepository) 
        {
            _AssetsRepository = AssetsRepository; 
        }

        public async Task<List<Patrimony>> GetPatrimonyByUser(Guid codeUser) 
        {
            return await _AssetsRepository.GetPatrimonyByUser(codeUser);
        }
    } 
} 
