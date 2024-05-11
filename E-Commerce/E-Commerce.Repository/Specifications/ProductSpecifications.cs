using E_Commerce.Core.Entity;
using E_Commerce.Core.SpecificationsParameters;

namespace E_Commerce.Repository.Specifications
{
	public class ProductSpecifications : BaseSpecification<Product>
	{
		public ProductSpecifications(ProductSpecificationParameters speci) : base(product =>
		(!speci.TypeId.HasValue || product.TypeId == speci.TypeId) &&
		(!speci.BrandId.HasValue || product.BrandId == speci.BrandId) &&
		(string.IsNullOrWhiteSpace(speci.Search) || product.Name == speci.Search))
		{
			IncluodsExpression.Add(product => product.ProductType);
			IncluodsExpression.Add(product => product.ProductBrand);
			if (speci.Sort is not null)
			{
				switch (speci.Sort)
				{
					case (ProductSortingParameters.NameAsc):
						OrderBy = p => p.Name;
						break;
					case (ProductSortingParameters.NameDesc):
						OrderbyDesc = p => p.Name;
						break;
					case (ProductSortingParameters.PriceAsc):
						OrderBy = p => p.Price;
						break;
					case (ProductSortingParameters.PriceDesc):
						OrderbyDesc = p => p.Price;
						break;

					default:
						OrderBy = p => p.Name;
						break;
				}
			}
			else
				OrderBy = p => p.Name;
			ApplyPaginated(speci.PageSize, speci.PageIndex);
		}
		public ProductSpecifications(int id) : base(product => product.Id == id)
		{
			IncluodsExpression.Add(product => product.ProductType);
			IncluodsExpression.Add(product => product.ProductBrand);
		}
	}
}
