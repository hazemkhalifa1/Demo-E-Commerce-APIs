namespace E_Commerce.Core.DataTransferObjects
{
	public class CustomerBasketDto
	{
		public string BasketID { get; set; }
		public int? DeliverMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
		public string PaymentIntentId { get; set; }
		public string ClientSecret { get; set; }
	}
}
