using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;

namespace E_Commerce.Services
{
	public class BasketService : IBasketService
	{
		private readonly IBasketRepository _repository;
		private readonly IMapper _mapper;

		public BasketService(IBasketRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<bool> DeleteBasketAsync(string id) => await _repository.DeleteBasketAsync(id);

		public async Task<CustomerBasketDto?> GetBasketAsync(string id)
			=> _mapper.Map<CustomerBasketDto>(await _repository.GetBasketAsync(id));

		public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto)
		{
			var CustomBasket = _mapper.Map<CustomerBasket>(basketDto);
			var updatedBaske = await _repository.UpdateBasketAsync(CustomBasket);
			return updatedBaske is null ? null : _mapper.Map<CustomerBasketDto>(updatedBaske);
		}
	}
}
