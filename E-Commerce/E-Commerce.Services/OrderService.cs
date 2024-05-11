using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity;
using E_Commerce.Core.Entity.OrderEntities;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Specifications;

namespace E_Commerce.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IBasketService _basketService;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService, IPaymentService paymentService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_basketService = basketService;
			_paymentService = paymentService;
		}

		public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
		{
			var basket = await _basketService.GetBasketAsync(input.BasketId);
			if (basket is null) throw new Exception($"No Basket With Id {input.BasketId} Was Found");

			// Get Order Items 
			var orderItems = new List<OrderItem>();
			foreach (var BasketItems in basket.BasketItems)
			{
				var product = await _unitOfWork.Repository<Product, int>().GetAsync(BasketItems.ProductId);
				if (product is null) continue;
				var productItem = new OIProduct
				{
					ProductId = product.Id,
					PictureUrl = product.PictureUrl,
					ProductName = product.Name
				};
				var orderItem = new OrderItem
				{
					OIProduct = productItem,
					Price = product.Price,
					Quantity = BasketItems.Quntity
				};
				orderItems.Add(orderItem);
			}

			//Get Delivery Method
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(input.DeliveryMethodId.Value);
			if (deliveryMethod is null) throw new Exception("No Delivery Method Was Selected");
			var mappedDeliveryMethod = _mapper.Map<DeliveryMethod>(deliveryMethod);

			// Get Shopping Address
			var shoppingAddress = _mapper.Map<ShoppinAddress>(input.ShoppinAddress);

			// SubTotal 
			var spec = new OrderPaymentIntentIdSpecification(basket.PaymentIntentId);
			var existOrder = await _unitOfWork.Repository<Order, Guid>().GetSpecAsync(spec);
			if (existOrder is not null)
			{
				await _unitOfWork.Repository<Order, Guid>().DeleteAsync(existOrder.Id);
				await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);
			}
			else
			{
				basket = await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(basket.BasketID);

			}
			var subTotal = orderItems.Sum(p => p.Price * p.Quantity);

			// Create Oredr
			var order = new Order
			{
				BuyerEmail = input.BuyerEmail,
				OrderItems = orderItems,
				DeliveryMethodId = deliveryMethod.Id,
				DeliveryMethod = mappedDeliveryMethod,
				ShoppinAddress = shoppingAddress,
				SubTotal = subTotal,
				PaymentIntentId = basket.PaymentIntentId,
				BaskettId = basket.BasketID
			};
			await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
			await _unitOfWork.CompleteAsync();
			return _mapper.Map<OrderResultDto>(order);
		}

		public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodAsync()
		=> await _unitOfWork.Repository<DeliveryMethod, int>().GetAllSpecAsync(new BaseSpecification<DeliveryMethod>(null));

		public async Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string email)
		{
			var orders = await _unitOfWork.Repository<Order, Guid>().GetAllSpecAsync(new OrderSpecification(email));
			if (!orders.Any()) throw new Exception("No Orders For This Email");
			return _mapper.Map<IEnumerable<OrderResultDto>>(orders);

		}

		public async Task<OrderResultDto> GetOrderAsync(Guid id, string email)
		{
			var orders = await _unitOfWork.Repository<Order, Guid>().GetAllSpecAsync(new OrderSpecification(id, email));
			if (!orders.Any()) throw new Exception("No Orders For This Email");
			return _mapper.Map<OrderResultDto>(orders);
		}
	}
}
