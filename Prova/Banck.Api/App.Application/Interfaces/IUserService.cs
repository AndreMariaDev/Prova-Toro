using App.Domain.Models;
using App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IUserService : IBaseService<User>
    {
        Task<UserView> GetByCodeAsync(Guid code);
        Task<IEnumerable<UserBank>> GetListUser();
    } 
} 
