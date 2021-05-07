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
    public class TradedController :  BaseController<Traded> 
    { 
        private readonly ITradedService appService; 
 
        public TradedController(ITradedService appService) : base(appService)  
        { 
            this.appService = appService; 
        } 
 
        [HttpPost] 
        [Authorize] 
        public override async Task<IActionResult> Insert([FromBody] Traded register)   
        { 
            return await TryExecuteAction(async () => 
            { 
                var result = await appService.Create(register); 
                return StatusCode((int)HttpStatusCode.Created, result); 
 
            }); 
        } 
    } 
} 
