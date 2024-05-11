namespace E_Commerce.Core.Entity.Basket
{
	public class CustomerBasket
	{
		public string BasketID { get; set; }
		public int? DeliverMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
	}
}
