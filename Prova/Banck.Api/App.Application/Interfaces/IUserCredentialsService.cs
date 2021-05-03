using App.Domain.Models;
using App.Shared.Extensions;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IUserCredentialsService : IBaseService<UserCredentials>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    } 
} 
