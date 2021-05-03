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
using App.Shared.Extensions;

namespace App.Infra.Data.Repositories 
{ 
    public class UserCredentialsRepository : Repository<UserCredentials>, IUserCredentialsRepository
    {
        #region Constructor
        public UserCredentialsRepository(BanckContext context) : base(context)
        {
 
        }
        #endregion

        public async Task<IEnumerable<UserCredentials>> GetAuthenticate(AuthenticateRequest model)
        {
            return this._context.Set<UserCredentials>().AsNoTracking().Where(x => x.Login == model.Username && x.Password == model.Password).Include(x => x.User);
        }
    } 
} 
