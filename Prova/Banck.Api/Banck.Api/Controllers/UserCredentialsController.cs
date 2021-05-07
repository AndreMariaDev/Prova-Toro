using System.Net;
using System.Threading.Tasks; 
using App.Application.Interfaces; 
using App.Domain.Models;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Banck.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class UserCredentialsController :  BaseController<UserCredentials> 
    { 
        private readonly IUserCredentialsService appService;
        
        public UserCredentialsController(IUserCredentialsService appService) : base(appService)  
        { 
            this.appService = appService;
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
    } 
} 
