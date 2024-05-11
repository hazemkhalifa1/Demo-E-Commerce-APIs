using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
		{
			var basket = await _basketService.GetBasketAsync(id);
			return basket is null ? NotFound(new ApiResponse(404, $"Basket {id} Not Found")) : Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
		{
			var basket = await _basketService.UpdateBasketAsync(basketDto);
			return basket is null ? NotFound(new ApiResponse(404, $"Basket {basketDto.BasketID} Not Found")) : Ok(basket);
		}

		[HttpDelete]
		public async Task<ActionResult> Delete(string id)
			=> Ok(await _basketService.DeleteBasketAsync(id));
	}
}
