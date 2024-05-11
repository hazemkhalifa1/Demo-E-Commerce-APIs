using E_Commerce.Core.Entity;
using E_Commerce.Core.SpecificationsParameters;

namespace E_Commerce.Repository.Specifications
{
	public class ProductCountWithSpec : BaseSpecification<Product>
	{
		public ProductCountWithSpec(ProductSpecificationParameters speci) : base(product =>
		(!speci.TypeId.HasValue || product.TypeId == speci.TypeId) &&
		(!speci.BrandId.HasValue || product.BrandId == speci.BrandId) &&
		(string.IsNullOrWhiteSpace(speci.Search) || product.Name == speci.Search))
		{
		}
	}
}
