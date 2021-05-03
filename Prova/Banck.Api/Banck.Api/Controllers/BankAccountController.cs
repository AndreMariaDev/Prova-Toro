using System;
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
    public class BankAccountController :  BaseController<BankAccount> 
    { 
        private readonly IBankAccountService appService; 
 
        public BankAccountController(IBankAccountService appService) : base(appService)  
        { 
            this.appService = appService; 
        } 
 
        [HttpPost] 
        [Authorize] 
        public override async Task<IActionResult> Insert([FromBody] BankAccount register)   
        { 
            return await TryExecuteAction(async () => 
            { 
                var result = await appService.Create(register); 
                return StatusCode((int)HttpStatusCode.Created, result); 
 
            }); 
        }

        [HttpGet("GetAmountInAccount")]
        [Authorize]
        public async Task<IActionResult> GetAmountInAccount(Guid codeUser)
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetAmountInAccount(codeUser);
                return StatusCode((int)HttpStatusCode.Created, result);

            });
        }
        [HttpGet("GetExistBankAccount")]
        [Authorize]
        public async Task<IActionResult> GetExistBankAccount(String accountNumber, String branch)
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetExistBankAccount(accountNumber, branch);
                return StatusCode((int)HttpStatusCode.Created, result);

            });
        }
        [HttpGet("GetBankAccountByUser")]
        [Authorize]
        public async Task<IActionResult> GetBankAccountByUser(Guid codeUser)
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.GetBankAccountByUser(codeUser);
                return StatusCode((int)HttpStatusCode.Created, result);

            });
        }
        
    }
} 
