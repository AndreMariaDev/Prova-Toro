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
    } 
} 
