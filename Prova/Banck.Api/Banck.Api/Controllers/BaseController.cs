using App.Application.Interfaces;
using App.Domain.Models;
using App.Shared.Exceptions;
using Banck.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Banck.Api.Controllers
{
	public abstract class BaseController<T> : ControllerBase where T : DomainEntity
	{
		#region Properts
		private readonly IBaseService<T> _service;
		//private readonly IAuthenticateService _authenticateService;
		#endregion

		#region Constructor
		public BaseController(IBaseService<T> service)
		{
			_service = service;
		}
		#endregion

		#region Actions
		[HttpGet]
		[Authorize]
		public virtual async Task<IActionResult> GetAll(int _offset = 1, int _limit = 10)
		{
			return await TryExecuteAction(async () =>
			{
				if (_offset > _limit)
					return StatusCode((int)HttpStatusCode.PreconditionFailed, "Bad Request!");
				T entity = CreateInstance();
				IEnumerable<T> result = null;
				if (entity.GetType().Name == "User")
				{
					result = await _service.GetAll();
				}
				else
				{
					result = await _service.FindAsync(x => x.IsActive == true);
				}

				if (null == result)
				{
					return StatusCode((int)HttpStatusCode.NoContent);
				}

				var skip = (_offset - 1) * _limit;

				var fields = GetParams(CreateInstance());
				if (fields.Contains("Order"))
				{
					result = this.Sort(result, "Order", "asc", "Order");
				}
				var list = result.Skip(skip).Take(_limit);

				var response = new OkObjectResult(list);
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Value = list;
				return response;
			});
		}

		[HttpGet("{id}")]
		////[Authorize]
		public virtual async Task<IActionResult> GetById(Guid? code)
		{
			if (code == null)
			{
				return BadRequest();
			}

			return await TryExecuteAction(async () =>
			{
				var result = await _service.FindAsync(x=> x.Code == code.GetValueOrDefault());

				if (null == result)
				{
					return new NotFoundResult();
				}
				var response = new OkObjectResult(result);
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Value = result;
				return response;
			});
		}

		[HttpPost]
		////[Authorize]
		public virtual async Task<IActionResult> Insert([FromBody] T register)
		{
			return await TryExecuteAction(async () =>
			{
				var result = await _service.Create(register);
				return StatusCode((int)HttpStatusCode.Created, result);

			});
		}

		[HttpPut("{id}")]
		////[Authorize]
		public virtual async Task<IActionResult> Update(Guid id, [FromBody] T register)
		{
			return await TryExecuteAction(async () =>
			{
				var result = await _service.Update(register);

				return StatusCode((int)HttpStatusCode.OK, result);

			});
		}

		[HttpDelete("{code}/{UserUpdate}")]
		////[Authorize]
		public virtual async Task<IActionResult> DeleteById(Guid code, Guid UserUpdate)
		{
			return await TryExecuteAction(async () =>
			{
				var entity = await _service.DeleteLogic(code, UserUpdate);
				//return StatusCode((int)HttpStatusCode.OK, true);
				return Ok(true);
			});
		}
		#endregion

		#region Methods
		protected string GetClaim(string type)
		{
			if (Request == null)
				return string.Empty;

			return Request.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == type)?.Value;
		}

		protected async Task<IActionResult> TryExecuteAction(Func<Task<IActionResult>> myMetho)
		{
			try
			{
				return await myMetho();
			}
			catch (BadRequestException ex)
			{
				LogBadRequest(ex);
				Console.WriteLine(String.Format("Exception TryExecuteAction:{0}", ex.Message));
				return BadRequest();
			}
			catch (Exception ex)
			{
				LogExeption(ex);
				return BadRequest();
			}
		}
		protected bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		private IActionResult LogBadRequest(BadRequestException ex)
		{
			return StatusCode((int)HttpStatusCode.PreconditionFailed, $"Bad Request! : {ex.Message}");
		}

		private IActionResult LogExeption(Exception ex)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno! : {ex.Message}");
		}

		private List<String> GetParams(T e)
		{
			Type type = e.GetType();
			List<PropertyInfo> properties = type.GetProperties().ToList();
			return properties.Select(x => x.Name).ToList<string>();
		}

		private T CreateInstance()
		{
			try
			{
				return System.Activator.CreateInstance<T>();
			}
			catch (MissingMethodException)
			{
				return default(T);
			}
		}

		private IEnumerable<T> Sort(IEnumerable<T> source, string sortBy, string sortDirection, String field)
		{
			var param = Expression.Parameter(typeof(T), field);

			var sortExpression = Expression.Lambda<Func<T, object>>
				(Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

			switch (sortDirection.ToLower())
			{
				case "asc":
					return source.AsQueryable<T>().OrderBy<T, object>(sortExpression);
				default:
					return source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);

			}
		}

		protected DateTime HrBrasiliam()
		{
			try
			{
				DateTime dateTime = DateTime.UtcNow;
				TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
				Console.WriteLine(String.Format("HrBrasiliam:{0}", hrBrasilia));
				return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
			}
			catch (Exception ex)
			{
				foreach (var item in TimeZoneInfo.GetSystemTimeZones())
				{
					Console.WriteLine(String.Format("TimeZoneInfo.Name:{0}; TimeZoneInfo.Id:{1}", item.DisplayName, item.Id));
				}
				Console.WriteLine(String.Format("Converter.Exception:{0}", ex.Message));
				return DateTime.UtcNow;
			}
		}
		#endregion

	}
}
