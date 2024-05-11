using E_Commerce.Core.Entity.Basket;

namespace E_Commerce.Core.Interfaces.Repositories
{
	public interface IBasketRepository
	{
		public Task<CustomerBasket?> GetBasketAsync(string id);
		public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket);
		public Task<bool> DeleteBasketAsync(string id);
	}
}
