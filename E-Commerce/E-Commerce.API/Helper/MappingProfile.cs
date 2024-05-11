using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity;
using E_Commerce.Core.Entity.Basket;

namespace E_Commerce.API.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ProductBrand, BrandTypeDto>().ReverseMap();
			CreateMap<ProductType, BrandTypeDto>().ReverseMap();


			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();

		}
	}
}
