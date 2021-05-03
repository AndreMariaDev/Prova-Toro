using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 
using App.Application.Interfaces; 
using App.Domain.Models;
using App.Shared.Extensions;
using Banck.Api.Configurations;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Banck.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class UserCredentialsController :  BaseController<UserCredentials> 
    { 
        private readonly IUserCredentialsService appService;
        private readonly AppSettings _appSettings;
        public UserCredentialsController(IUserCredentialsService appService, IOptions<AppSettings> appSettings) : base(appService)  
        { 
            this.appService = appService;
            this._appSettings = appSettings.Value;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await appService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            response.Token = this.GenerateJwtToken(response.Code);

            return Ok(response);
        }


        [HttpPost] 
        [Authorize] 
        public override async Task<IActionResult> Insert([FromBody] UserCredentials register)   
        { 
            return await TryExecuteAction(async () => 
            { 
                var result = await appService.Create(register); 
                return StatusCode((int)HttpStatusCode.Created, result); 
 
            }); 
        }

        private string GenerateJwtToken(Guid userCode)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userCode.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    } 
} 
