using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.SpecificationsParameters;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IProductService
	{
		Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specParameters);
		Task<ProductToReturnDto> GetProductAsync(int id);
		Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
		Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();

	}
}
