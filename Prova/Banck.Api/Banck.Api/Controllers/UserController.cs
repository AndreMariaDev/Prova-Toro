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
    } 
} 
