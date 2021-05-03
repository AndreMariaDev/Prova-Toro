using App.Domain.Models;
using App.Infra.Data.Context;
using App.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace App.Infra.Data.Repositories 
{ 
    public class AssetsRepository : Repository<Assets>, IAssetsRepository
    {
        #region Constructor
        public AssetsRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion

        public async Task<List<Patrimony>> GetPatrimonyByUser(Guid codeUser)
        {
            decimal totalValuesAssets = 0;
            var itensAssets = this._context.Set<Assets>().AsNoTracking().Where(x => x.UserCode == codeUser);
            if (null != itensAssets) 
            {
                var resultItensAssets = await itensAssets.ToListAsync();
                foreach (var item in resultItensAssets)
                {
                    totalValuesAssets =  totalValuesAssets + (item.Value * (item.Amount == 0 ? 1 : item.Amount));
                }
            }
            
            var listAssets = await this._context.Set<Assets>().AsNoTracking().Where(x => x.UserCode == codeUser).ToListAsync();

            var result = (from bankAccount in this._context.Set<BankAccount>().AsNoTracking()
                          where bankAccount.UserCode == codeUser
                          select new Patrimony() {
                              AccountNumber = bankAccount.AccountNumber,
                              Branch = bankAccount.Branch,
                              Balance = bankAccount.Amount,
                              SummarizedEquity = (totalValuesAssets + bankAccount.Amount),
                              ListAssets = listAssets,
                              UserCode = bankAccount.UserCode,
                              Name = this._context.Set<User>().AsNoTracking().FirstOrDefault(x=> x.Code == codeUser).Name
                          });
            return result != null ? await result.ToListAsync() : null;
        }

        public async Task<Boolean> AddAsset(Assets entity) 
        {
            using (var transaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    var Account = this._context.Set<BankAccount>().AsNoTracking().FirstOrDefault(x => x.UserCode == entity.UserCode);
                    if (null != Account)
                    {
                        var history = new BankAccountHistory();
                        history.BankAccountCode = Account.Code;
                        history.MovingDate = DateTime.Now;
                        history.TypeMoving = enumTypeMoving.BankDraft;
                        history.AmountMoved = entity.Value;
                        history.Create = DateTime.Now;
                        history.IsActive = true;
                        history.Code = Guid.NewGuid();
                        history.UserCreate = entity.UserCode;

                        await this._context.Set<BankAccountHistory>().AddAsync(history);
                        await this._context.SaveChangesAsync();

                        await this._context.Set<Assets>().AddAsync(entity);
                        await this._context.SaveChangesAsync();

                        var amount = Account.Amount;
                        Account.Amount = amount - (entity.Value * (entity.Amount == 0 ? 1: entity.Amount));

                        this._context.Set<BankAccount>().Update(Account);
                        await this._context.SaveChangesAsync();

                        transaction.Commit();
                        return true;
                    }
                    return false;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error(String.Format("Error Method AddAsset: {0}", ex.Message));
                    throw;
                }
            }
        }
    } 
} 
