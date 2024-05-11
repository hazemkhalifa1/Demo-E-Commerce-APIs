using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.SpecificationsParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}
		[Authorize]
		[HttpGet]
		[Cash(60)]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecificationParameters specParameters)
			=> Ok(await _productService.GetAllProductsAsync(specParameters));

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _productService.GetProductAsync(id);
			return product is not null ? Ok(product) : NotFound(new ApiResponse(404, $"Product With Id {id} Not Found"));
		}

		[HttpGet("Brands")]
		public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetBrands() => Ok(await _productService.GetAllBrandsAsync());

		[HttpGet("Types")]
		public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetTypes() => Ok(await _productService.GetAllTypesAsync());
	}
}
