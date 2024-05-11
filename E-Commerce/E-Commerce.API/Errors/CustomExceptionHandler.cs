using System.Net;

namespace E_Commerce.API.Errors
{
	public class CustomExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandler> _logger;
		private readonly IHostEnvironment _environment;

		public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger, IHostEnvironment environment)
		{
			_next = next;
			_logger = logger;
			_environment = environment;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				_logger.LogError(ex.Message);
				var response = _environment.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
					: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				await context.Response.WriteAsJsonAsync(response);
			}
		}
	}
}
