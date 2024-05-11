using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DataTransferObjects
{
	public class BasketItemDto
	{
		[Required]
		public int ProductId { get; set; }
		[Required]
		public string productName { get; set; }
		[Required]
		public string Dsscription { get; set; }
		[Required]
		[Range(1, 99)]
		public int Quntity { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		public string TypeName { get; set; }
		[Required]
		public string BrandName { get; set; }
	}
}
