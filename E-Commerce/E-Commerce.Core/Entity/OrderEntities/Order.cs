namespace E_Commerce.Core.Entity.OrderEntities
{
	public class Order : BaseEntity<Guid>
	{
		public string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		public ShoppinAddress ShoppinAddress { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public int? DeliveryMethodId { get; set; }
		public IEnumerable<OrderItem> OrderItems { get; set; }
		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
		public decimal SubTotal { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? BaskettId { get; set; }
		public decimal Total => SubTotal + DeliveryMethod.Price;
	}
}
