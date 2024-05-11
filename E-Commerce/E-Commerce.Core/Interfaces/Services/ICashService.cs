namespace E_Commerce.Core.Interfaces.Services
{
	public interface ICashService
	{
		public Task<string?> GetCashResponseAsync(string cashId);
		public Task SetCashResponseAsync(string cashId, object response, TimeSpan time);
	}
}
