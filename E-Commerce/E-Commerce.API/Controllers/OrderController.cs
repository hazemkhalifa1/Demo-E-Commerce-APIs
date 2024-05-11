using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.OrderEntities;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<ActionResult<OrderResultDto>> Create(OrderDto orderResultDto)
		{
			if (orderResultDto is null) throw new Exception("No Order To Create");
			var order = await _orderService.CreateOrderAsync(orderResultDto);
			return Ok(order);
		}
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrders()
		{
			var orders = await _orderService.GetAllOrdersAsync(User.FindFirstValue(ClaimTypes.Email));
			return Ok(orders);
		}
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResultDto>> GetOrder(Guid id)
		{
			var order = await _orderService.GetOrderAsync(id, User.FindFirstValue(ClaimTypes.Email));
			if (order is null) throw new Exception($"No Order With Id {id}");
			return Ok(order);
		}

		[HttpGet("Delivery")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
		{
			var deliveryMethod = await _orderService.GetAllDeliveryMethodAsync();
			if (deliveryMethod is null) throw new Exception("No DeliveryMethodes");
			return Ok(deliveryMethod);
		}
	}
}
