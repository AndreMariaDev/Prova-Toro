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
    public class AssetsController :  BaseController<Assets> 
    { 
        private readonly IAssetsService appService; 
 
        public AssetsController(IAssetsService appService) : base(appService)  
        { 
            this.appService = appService; 
        } 
 
        [HttpPost]
        [Authorize]
        public override async Task<IActionResult> Insert([FromBody] Assets register)   
        { 
            return await TryExecuteAction(async () => 
            { 
                var result = await appService.CreateCuston(register);
                if (null != result)
                    return StatusCode((int)HttpStatusCode.Created, result);
                else 
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Insufficient funds");
                }
            }); 
        } 
    } 
} 
