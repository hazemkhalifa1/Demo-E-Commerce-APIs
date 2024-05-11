using E_Commerce.Core.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Services
{
	public class CashService : ICashService
	{
		private readonly IDatabase _database;

		public CashService(IConnectionMultiplexer connection)
		{
			_database = connection.GetDatabase();
		}
		public async Task<string?> GetCashResponseAsync(string cashId)
		{
			var response = await _database.StringGetAsync(cashId);
			return response.IsNullOrEmpty ? null : response.ToString();
		}

		public async Task SetCashResponseAsync(string cashId, object response, TimeSpan time)
		{
			var serializedResp = JsonSerializer.Serialize(response
				, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			await _database.StringSetAsync(cashId, serializedResp, time);
		}
	}
}
