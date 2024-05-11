using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IOrderService
	{
		public Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodAsync();
		public Task<OrderResultDto> CreateOrderAsync(OrderDto input);
		public Task<OrderResultDto> GetOrderAsync(Guid id, string email);
		public Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string email);

	}
}
