using App.Domain.Models;
using System.Threading.Tasks;

namespace App.Application.Interfaces 
{ 
    public interface IAssetsService : IBaseService<Assets>
    {
        Task<bool> CreateCuston(Assets entity);
    } 
} 
