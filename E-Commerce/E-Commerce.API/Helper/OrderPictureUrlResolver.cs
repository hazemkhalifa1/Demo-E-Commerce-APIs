using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.API.Helper
{
	public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
			=> string.IsNullOrWhiteSpace(source.OIProduct.PictureUrl) ? string.Empty : $@"{_configuration["BaseUrl"]}{source.OIProduct.PictureUrl}";
	}
}
