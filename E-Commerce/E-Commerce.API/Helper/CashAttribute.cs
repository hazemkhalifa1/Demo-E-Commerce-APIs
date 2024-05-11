using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Helper
{
	public class CashAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _time;
		public CashAttribute(int time)
		{
			_time = time;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cashKey = GenerateKeyFromRequest(context.HttpContext.Request);
			var _cashServise = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
			var _cashResponse = await _cashServise.GetCashResponseAsync(cashKey);
			if (_cashResponse != null)
			{
				context.Result = new ContentResult
				{
					ContentType = "application/json",
					StatusCode = 200,
					Content = _cashResponse
				};
				return;
			}
			var executedContext = await next();
			if (executedContext.Result is OkObjectResult response)
				await _cashServise.SetCashResponseAsync(cashKey, response, TimeSpan.FromSeconds(_time));
			return;
		}


		private string GenerateKeyFromRequest(HttpRequest request)
		{
			StringBuilder key = new StringBuilder();
			key.Append($"{request.Path}");
			foreach (var item in request.Query.OrderBy(k => k.Key))
				key.Append(item);
			return key.ToString();
		}
	}
}
