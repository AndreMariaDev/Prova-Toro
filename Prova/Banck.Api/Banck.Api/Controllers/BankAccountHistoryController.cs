using System.Net;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountHistoryController : BaseController<BankAccountHistory>
    {
        private readonly IBankAccountHistoryService appService;

        public BankAccountHistoryController(IBankAccountHistoryService appService) : base(appService)
        {
            this.appService = appService;
        }

        [HttpPost]
        //[Authorize] 
        public override async Task<IActionResult> Insert([FromBody] BankAccountHistory register)
        {
            return await TryExecuteAction(async () =>
            {
                var result = await appService.Create(register);
                return StatusCode((int)HttpStatusCode.Created, result);

            });
        }
    }
}