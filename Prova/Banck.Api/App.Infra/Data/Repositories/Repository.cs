
using App.Domain.Models;
using App.Infra.Data.Context;
using App.Infra.Data.Interfeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;


namespace App.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : DomainEntity
    {
        protected readonly BanckContext _context;
        protected readonly DbSet<T> DbSet;

        public Repository(BanckContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        #region Private
        private IQueryable<T> FindAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
        private IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var query = this._context.Set<T>()
                .Where(expression).AsNoTracking();

            return query;
        }
        #endregion

        #region MethodsGet
        public async Task<T> GetByCodeAsync(Guid code)
        {
            return await DbSet.FindAsync(code);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await FindAll()
                   .Where(predicate)
                   .ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await FindAll().ToListAsync();
        }
        #endregion

        #region MethodsCrud
        public async Task<T> Create(T entity)
        {
            try
            {
                await this._context.Set<T>().AddAsync(entity);
                await this._context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                throw;
            }

        }
        public async Task<T> Update(T entity)
        {
            this._context.Set<T>().Update(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }
        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public async Task<String> DeleteLogic(Guid Code,Guid UserUpdate) 
        {
            try
            {
                var item = this._context.Set<T>()
                    .FirstOrDefault(x => x.Code == Code);
                item.IsActive = false;
                item.UserUpdate = UserUpdate;
                item.Update = DateTime.Now;
                await this.Update(item);
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
