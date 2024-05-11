using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.OrderEntities;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = E_Commerce.Core.Entity.Product;

namespace E_Commerce.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IBasketService _basketService;
		private readonly IConfiguration _configuration;

		public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_basketService = basketService;
			_configuration = configuration;
		}

		public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto basket)
		{
			StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

			foreach (var item in basket.BasketItems)
			{
				var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);
				if (product.Price != item.Price)
					item.Price = product.Price;
			}
			var total = basket.BasketItems.Sum(b => b.Price);

			if (!basket.DeliverMethodId.HasValue) throw new Exception("No DeliveryMethod Selected");
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliverMethodId.Value);
			var shoppingPrice = deliveryMethod.Price;
			basket.ShippingPrice = deliveryMethod.Price;

			long amount = (long)((total + shoppingPrice) * 100);

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
			{
				//Create
				var option = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "usd",
					PaymentMethodTypes = new List<string> { "card" }
				};
				paymentIntent = await service.CreateAsync(option);

				basket.ClientSecret = paymentIntent.ClientSecret;
				basket.PaymentIntentId = paymentIntent.Id;
			}
			else
			{
				//Update
				var option = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				await service.UpdateAsync(basket.PaymentIntentId, option);
			}

			await _basketService.UpdateBasketAsync(basket);
			return basket;
		}

		public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

			var basket = await _basketService.GetBasketAsync(basketId);

			foreach (var item in basket.BasketItems)
			{
				var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);
				if (product.Price != item.Price)
					item.Price = product.Price;
			}
			var total = basket.BasketItems.Sum(b => b.Price);

			if (!basket.DeliverMethodId.HasValue) throw new Exception("No DeliveryMethod Selected");
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliverMethodId.Value);
			var shoppingPrice = deliveryMethod.Price;
			basket.ShippingPrice = deliveryMethod.Price;

			long amount = (long)((total + shoppingPrice) * 100);

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
			{
				//Create
				var option = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "usd",
					PaymentMethodTypes = new List<string> { "card" }
				};
				paymentIntent = await service.CreateAsync(option);

				basket.ClientSecret = paymentIntent.ClientSecret;
				basket.PaymentIntentId = paymentIntent.Id;
			}
			else
			{
				//Update
				var option = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				await service.UpdateAsync(basket.PaymentIntentId, option);
			}

			await _basketService.UpdateBasketAsync(basket);
			return basket;
		}

		public async Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId)
		{
			var spec = new OrderPaymentIntentIdSpecification(paymentIntentId);
			var order = await _unitOfWork.Repository<Order, Guid>().GetSpecAsync(spec);
			if (order is not null) throw new Exception($"No Order With PaymentIntentId {paymentIntentId}");
			order.PaymentStatus = PaymentStatus.Failed;
			_unitOfWork.Repository<Order, Guid>().Update(order);
			await _unitOfWork.CompleteAsync();
			return _mapper.Map<OrderResultDto>(order);
		}

		public async Task<OrderResultDto> UpdatePaymentStatusSucceded(string paymentIntentId)
		{
			var spec = new OrderPaymentIntentIdSpecification(paymentIntentId);
			var order = await _unitOfWork.Repository<Order, Guid>().GetSpecAsync(spec);
			if (order is null) throw new Exception($"No Order With PaymentIntentId {paymentIntentId}");
			order.PaymentStatus = PaymentStatus.Received;
			_unitOfWork.Repository<Order, Guid>().Update(order);
			await _unitOfWork.CompleteAsync();
			await _basketService.DeleteBasketAsync(order.BaskettId);
			return _mapper.Map<OrderResultDto>(order);
		}
	}
}
