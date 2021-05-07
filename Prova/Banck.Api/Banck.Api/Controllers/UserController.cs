using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks; 
using App.Application.Interfaces; 
using App.Domain.Models;
using App.Shared.Extensions;
using Banck.Api.Configurations;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc; 
 
namespace Banck.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class UserController :  BaseController<User> 
    { 
        private readonly IUserService appService; 
 
        public UserController(IUserService appService) : base(appService)  
        { 
            this.appService = appService; 
        }

        [HttpPost] 
        //[Authorize] 
        public override async Task<IActionResult> Insert([FromBody] User register)   
        { 
            return await TryExecuteAction(async () => 
            { 
                var result = await appService.Create(register); 
                return StatusCode((int)HttpStatusCode.Created, result); 
 
            }); 
        }

        [HttpGet("GetByCodeAsync")]
        [Authorize]
        public async Task<IActionResult> GetByCodeAsync(Guid code) 
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetByCodeAsync(code);
                if (null != result) {
                    result.Password = "";
                }
                return StatusCode((int)HttpStatusCode.Created, result);
            });
        }

        [HttpGet("GetListUser")]
        [Authorize]
        public async Task<IActionResult> GetListUser()
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetListUser();
                return StatusCode((int)HttpStatusCode.Created, result);
            });
        }

        [HttpGet("GetListUserByCode")]
        [Authorize]
        public async Task<IActionResult> GetListUserByCode(Guid codeUser)
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetAll();
                if (null != result)
                {
                    List<User> users = new List<User>();
                    users.Add(result.FirstOrDefault(x => x.Code == codeUser));
                    return StatusCode((int)HttpStatusCode.Created, users);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Insufficient funds");
                }
            });
        }
    } 
} 
