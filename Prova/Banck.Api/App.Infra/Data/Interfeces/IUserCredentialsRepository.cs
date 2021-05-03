using App.Domain.Models;
using App.Infra.Data.Interfeces;
using App.Shared.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Data.Interfaces 
{ 
    public interface IUserCredentialsRepository : IRepository<UserCredentials>
    {
        Task<IEnumerable<UserCredentials>> GetAuthenticate(AuthenticateRequest model);
    } 
} 
