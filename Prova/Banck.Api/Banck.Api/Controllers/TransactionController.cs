using App.Application.Interfaces;
using App.Shared.Exceptions;
using App.Shared.Extensions;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Banck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly IBankAccountService appService;

        public TransactionController(IBankAccountService appService)
        {
            this.appService = appService;
        }


        [HttpPost("RunTransaction")]
        [Authorize]
        public async Task<IActionResult> RunTransaction(TransactionHistoryResponse entity)
        {
            try
            {
                var result = await this.appService.RunTransaction(entity);
                return StatusCode((int)HttpStatusCode.Created, result);
            }
            catch (BadRequestException ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }
    }
}
