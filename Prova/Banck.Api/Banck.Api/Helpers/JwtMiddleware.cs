using App.Application.Interfaces;
using Banck.Api.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banck.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly AppSettings _appSettings;
        private readonly RequestDelegate _next;

        public JwtMiddleware(IConfiguration Configuration, RequestDelegate next)
        {
            if (null != Configuration)
            {
                this._appSettings = new AppSettings();
                new ConfigureFromConfigurationOptions<AppSettings>(Configuration.GetSection("AppSettings"))
                .Configure(_appSettings);
            }
            this._next = next;
        }

        public async Task Invoke(HttpContext context, IUserService appService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (null != token)
            {
                await this.AttachToContext(context, token, appService);
            }
            await this._next(context);
        }

        private async Task AttachToContext(HttpContext context, String token, IUserService appService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromMinutes(30)
                    },
                    out SecurityToken validateToken);

                var jwtToken = (JwtSecurityToken)validateToken;
                Guid IdUser;
                Guid.TryParse(jwtToken.Claims.First(x => x.Type == "id").Value, out IdUser);
                context.Items["User"] = await appService.GetByCodeAsync(IdUser);
            }
            catch
            {

            }
        }
    }
}
