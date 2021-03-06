using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> GetByCodeAsync(Guid code);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
        Task<String> DeleteLogic(Guid Code, Guid UserUpdate);
    }
}
