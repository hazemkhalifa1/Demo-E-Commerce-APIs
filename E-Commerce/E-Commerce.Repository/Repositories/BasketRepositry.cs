using E_Commerce.Core.Entity.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Repository.Repositories
{
	public class BasketRepositry : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepositry(IConnectionMultiplexer connection)
		{
			_database = connection.GetDatabase();
		}

		public async Task<bool> DeleteBasketAsync(string id) => await _database.KeyDeleteAsync(id);

		public async Task<CustomerBasket?> GetBasketAsync(string id)
		{
			var basket = await _database.StringGetAsync(id);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
		{
			var SerializedBasket = JsonSerializer.Serialize(customerBasket);
			var result = await _database.StringSetAsync(customerBasket.BasketID, SerializedBasket, TimeSpan.FromDays(4));
			return result ? customerBasket : null;
		}
	}
}
