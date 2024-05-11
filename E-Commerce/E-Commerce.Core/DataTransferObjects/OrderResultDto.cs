using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.Core.DataTransferObjects
{
	public class OrderResultDto
	{
		public Guid Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; }
		public AddressDto ShoppinAddress { get; set; }
		public string DeliveryMethodName { get; set; }
		public IEnumerable<OrderItemDto> OrderItems { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public decimal SubTotal { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? BaskettId { get; set; }
		public decimal ShoppingPrice { get; set; }
		public decimal Total { get; set; }
	}
}
