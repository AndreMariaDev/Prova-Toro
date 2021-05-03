using App.Application.Interfaces; 
using App.Domain.Models;
using App.Shared.Extensions;
using App.Infra.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Application.Services 
{ 
    public class UserService : BaseService<User>, IUserService 
    { 
        private readonly IUserRepository _UserRepository;
 
        public UserService(IUserRepository UserRepository) :base(UserRepository) 
        { 
            _UserRepository = UserRepository; 
        }

        public new async Task<UserView> GetByCodeAsync(Guid code) 
        {
            UserView userView = null;
            var user = await _UserRepository.GetUserForAuthenticate(code);
            if (null != user) 
            {
                var userCredentials = user.UserCredentialsItens.FirstOrDefault();
                return new UserView()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Login = userCredentials.Login,
                    Password = userCredentials.Password,
                    CredentialsType = (App.Shared.Extensions.enumCredentialsType)(int)userCredentials.CredentialsType
                };
            }
            return userView;
        }
    } 
} 
