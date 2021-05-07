using App.Domain.Models;
using App.Infra.Data.Interfeces;
using App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserForAuthenticate(Guid code);
        Task<IEnumerable<UserBank>> GetListUser();
    } 
} 
