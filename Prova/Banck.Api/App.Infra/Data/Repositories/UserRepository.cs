using App.Domain.Models;
using App.Infra.Data.Context;
using App.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repositories 
{ 
    public class UserRepository : Repository<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion

        public async Task<User> GetUserForAuthenticate(Guid code)
        {
            var result = this._context.Set<User>().AsNoTracking()
                .Where(x => x.Code == code)
                .Include(x => x.UserCredentialsItens);
            return result != null ? await result.FirstOrDefaultAsync() : null;
        }
    } 
} 
