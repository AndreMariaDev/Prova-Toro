using App.Domain.Models;
using App.Infra.Data.Context;
using App.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using App.Shared.Extensions;
using App.Infra.Data.Repositories;

namespace App.Infra.Data.Repository 
{ 
    public class BankAccountHistoryRepository : Repository<BankAccountHistory>, IBankAccountHistoryRepository
    {
        #region Constructor
        public BankAccountHistoryRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion

        public async Task<List<BankAccountHistory>> GetBankAccountHistoryByUser(Guid codeUser)
        {
            var result = (from a in this._context.Set<BankAccountHistory>().AsNoTracking()
                          join t in this._context.Set<BankAccount>().AsNoTracking()
                          on a.BankAccountCode equals t.Code into all
                          from top in all.DefaultIfEmpty()
                          where top.UserCode == codeUser
                          select a); 

            return result != null ? await result.ToListAsync() : null;
        }
    } 
} 
