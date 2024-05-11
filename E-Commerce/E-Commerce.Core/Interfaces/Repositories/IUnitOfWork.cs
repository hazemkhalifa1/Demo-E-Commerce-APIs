using E_Commerce.Core.Entity;

namespace E_Commerce.Core.Interfaces.Repositories
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
		Task<int> CompleteAsync();
	}
}
