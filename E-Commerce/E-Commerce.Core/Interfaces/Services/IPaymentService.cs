using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IPaymentService
	{
		public Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input);
		public Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);
		public Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId);
		public Task<OrderResultDto> UpdatePaymentStatusSucceded(string paymentIntentId);
	}
}
