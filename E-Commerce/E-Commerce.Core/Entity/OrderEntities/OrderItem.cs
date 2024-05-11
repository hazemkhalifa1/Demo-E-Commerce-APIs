namespace E_Commerce.Core.Entity.OrderEntities
{
	public class OrderItem : BaseEntity<Guid>
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public OIProduct OIProduct { get; set; }
	}
}