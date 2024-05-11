using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IBasketService
	{
		Task<CustomerBasketDto> GetBasketAsync(string id);
		Task<bool> DeleteBasketAsync(string id);
		Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto);
	}
}
