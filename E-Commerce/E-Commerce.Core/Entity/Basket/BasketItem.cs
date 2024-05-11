namespace E_Commerce.Core.Entity.Basket
{
	public class BasketItem
	{
		public int ProductId { get; set; }
		public string productName { get; set; }
		public string Dsscription { get; set; }
		public int Quntity { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		public string TypeName { get; set; }
		public string BrandName { get; set; }

	}
}
