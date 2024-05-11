using System.Text.Json.Serialization;

namespace E_Commerce.Core.SpecificationsParameters
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum ProductSortingParameters
	{
		NameAsc, NameDesc, PriceAsc, PriceDesc
	}
	public class ProductSpecificationParameters
	{
		public const int MAXPSGESIZE = 10;
		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public ProductSortingParameters? Sort { get; set; }
		public int PageIndex { get; set; } = 1;
		private int _pageSize = 5;
		private string? _search;

		public string? Search
		{
			get => _search;
			set => _search = value?.Trim().ToLower();
		}


		public int PageSize
		{
			get => _pageSize;
			set { _pageSize = value > MAXPSGESIZE ? MAXPSGESIZE : value; }
		}

	}
}
