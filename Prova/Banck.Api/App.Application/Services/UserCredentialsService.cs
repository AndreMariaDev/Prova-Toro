using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces;
using App.Shared.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.Services 
{ 
    public class UserCredentialsService : BaseService<UserCredentials>, IUserCredentialsService 
    { 
        private readonly IUserCredentialsRepository _UserCredentialsRepository;
        

        public UserCredentialsService(IUserCredentialsRepository UserCredentialsRepository) :base(UserCredentialsRepository) 
        { 
            _UserCredentialsRepository = UserCredentialsRepository; 
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var result = await _UserCredentialsRepository.GetAuthenticate(model);
            if (null != result) 
            {
                var user = result.FirstOrDefault();
                return new AuthenticateResponse()
                {
                    Code = user.User.Code,
                    Name = user.User.Name,
                    Email = user.User.Email,
                    Login = user.Login,
                };
            }
            return null;
        }
    } 
} 
