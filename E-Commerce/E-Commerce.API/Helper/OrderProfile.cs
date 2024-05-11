using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.API.Helper
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<ShoppinAddress, AddressDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(des => des.ProductName, sor => sor.MapFrom(o => o.OIProduct.ProductName))
				.ForMember(des => des.ProductId, sor => sor.MapFrom(o => o.OIProduct.ProductId))
				.ForMember(des => des.PictureUrl, sor => sor.MapFrom<OrderPictureUrlResolver>());
			CreateMap<Order, OrderResultDto>()
				.ForMember(des => des.DeliveryMethodName, opt => opt.MapFrom(opt => opt.DeliveryMethod.ShortName))
				.ForMember(des => des.ShoppingPrice, opt => opt.MapFrom(opt => opt.DeliveryMethod.Price));
		}
	}
}
