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
    public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        #region Constructor
        public BankAccountRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion
        public async Task<Boolean> GetExistBankAccount(String accountNumber,String branch)
        {
            return await this._context.Set<BankAccount>().AsNoTracking()
                .AnyAsync(x => x.AccountNumber == accountNumber && x.Branch == branch);
        }

        public async Task<List<BankAccount>> GetBankAccountByUser(Guid codeUser)
        {
            var result = this._context.Set<BankAccount>().AsNoTracking()
                .Where(x => x.UserCode == codeUser);
            return result != null ? await result.ToListAsync() : null;
        }

        public async Task<Boolean> RunTransaction(App.Shared.TransactionHistory entity)
        {
            using (var transaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    var Account = await this._context.Set<BankAccount>().AsNoTracking().FirstOrDefaultAsync(x => x.AccountNumber == entity.TargetAccount && x.Branch == entity.TargetBranch);
                    if (null != Account)
                    {
                        var history = new BankAccountHistory();
                        history.BankAccountCode = Account.Code;
                        history.MovingDate = DateTime.Now;
                        if (entity.OriginBranch == String.Format("{0}/ {1}", entity.TargetBranch, entity.TargetAccount))
                        {
                            history.TypeMoving = enumTypeMoving.BankDeposit;
                        }
                        else
                        {
                            history.TypeMoving = enumTypeMoving.BankDraft;
                        }
                        history.AmountMoved = entity.Amount;
                        history.Create = DateTime.Now;
                        history.IsActive = true;
                        history.Code = Guid.NewGuid();
                        history.UserCreate = Account.UserCode;

                        await this._context.Set<BankAccountHistory>().AddAsync(history);
                        await this._context.SaveChangesAsync();

                        var amount = Account.Amount;

                        Account.Amount = amount + entity.Amount;

                        this._context.Set<BankAccount>().Update(Account);
                        await this._context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return true;
                    }
                    return false;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error(String.Format("Error Method AddAsset: {0}", ex.Message));
                    return false;
                }
                finally {
                    await transaction.DisposeAsync();
                }
            }
        }
    } 
} 
