
namespace E_Commerce.API.Errors
{
	public class ApiResponse
	{
		public ApiResponse(int statusCode, string? errorMessage = null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage ?? GetErrorMessage(StatusCode);
		}

		private string? GetErrorMessage(int statusCode)
		=> statusCode switch
		{
			500 => "Internal Server Error",
			400 => "Bad Request",
			404 => "Not Found",
			401 => "UnAuthorized",
			_ => ""
		};

		public int StatusCode { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
