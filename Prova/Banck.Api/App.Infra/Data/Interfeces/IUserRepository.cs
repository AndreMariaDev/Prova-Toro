using App.Domain.Models;
using App.Infra.Data.Interfeces;
using System;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserForAuthenticate(Guid code);
    } 
} 
