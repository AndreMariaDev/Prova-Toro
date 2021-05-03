using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IPatrimonyService
    {
        Task<List<Patrimony>> GetPatrimonyByUser(Guid codeUser);
    } 
} 
