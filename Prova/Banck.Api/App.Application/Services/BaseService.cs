using App.Domain.Models;
using App.Application.Interfaces;
using App.Infra.Data.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : DomainEntity, new()
    {
        #region Properts
        private IRepository<T> Repository { get; set; }
        #endregion

        #region Constructor
        public BaseService(IRepository<T> repository)
        {
            Repository = repository;
        }
        #endregion

        #region MethodsGet
        public async Task<T> GetByCodeAsync(Guid code)
        {
            return await Repository.GetByCodeAsync(code);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Repository.FindAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Repository.GetAll();
        }
        #endregion

        #region MethodsCrud
        public virtual async Task<T> Create(T entity)
        {
            return await Repository.Create(entity);
        }
        public async Task<T> Update(T entity)
        {
            return await Repository.Update(entity); 
        }
        public void Delete(T entity)
        {
            Repository.Delete(entity);
        }
        public async Task<String> DeleteLogic(Guid Code, Guid UserUpdate) 
        {
            return await Repository.DeleteLogic(Code, UserUpdate);
        }
        #endregion
    }
}
