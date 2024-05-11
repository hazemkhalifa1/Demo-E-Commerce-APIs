
using E_Commerce.Core.Entity;
using E_Commerce.Core.Interfaces.Specifications;

namespace E_Commerce.Core.Interfaces.Repositories
{
	public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		Task<IReadOnlyList<TEntity>> GetAllSpecAsync(ISpecification<TEntity> specification);
		Task<int> GetProductCountSpscAsync(ISpecification<TEntity> specification);
		Task<TEntity> GetAsync(TKey id);
		Task<TEntity> GetSpecAsync(ISpecification<TEntity> specification);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		Task DeleteAsync(TKey id);
	}
}
