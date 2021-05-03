using System;
using System.Net;
using System.Threading.Tasks; 
using App.Application.Interfaces; 
using App.Domain.Models;
using App.Shared.Exceptions;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Banck.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class PatrimonyController : ControllerBase
    { 
        private readonly IPatrimonyService appService; 
 
        public PatrimonyController(IPatrimonyService appService)
        { 
            this.appService = appService; 
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetPatrimonyByUser(Guid codeUser)
        {
            try
            {
                var result = await this.appService.GetPatrimonyByUser(codeUser);
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
