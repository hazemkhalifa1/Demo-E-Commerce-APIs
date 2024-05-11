using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.SpecificationsParameters;
using E_Commerce.Repository.Specifications;

namespace E_Commerce.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
		{
			var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllSpecAsync(new BaseSpecification<ProductBrand>(null));
			return _mapper.Map<IEnumerable<BrandTypeDto>>(brands);
		}

		public async Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specParameters)
		{
			var spec = new ProductSpecifications(specParameters);
			var products = await _unitOfWork.Repository<Product, int>().GetAllSpecAsync(spec);
			var coutSpec = new ProductCountWithSpec(specParameters);
			var count = await _unitOfWork.Repository<Product, int>().GetProductCountSpscAsync(coutSpec);
			var mappedProduct = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
			return new PaginatedResultDto<ProductToReturnDto>
			{
				Data = mappedProduct,
				PageIndex = specParameters.PageIndex,
				PageSize = specParameters.PageSize,
				TotalCount = count
			};
		}

		public async Task<ProductToReturnDto> GetProductAsync(int id)
		{
			var spec = new ProductSpecifications(id);
			var product = await _unitOfWork.Repository<Product, int>().GetSpecAsync(spec);
			return _mapper.Map<ProductToReturnDto>(product);
		}

		public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
		{
			var types = await _unitOfWork.Repository<ProductType, int>().GetAllSpecAsync(new BaseSpecification<ProductType>(null));
			return _mapper.Map<IEnumerable<BrandTypeDto>>(types);
		}
	}
}
