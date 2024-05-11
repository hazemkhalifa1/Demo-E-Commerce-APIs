using E_Commerce.Core.Entity;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Repository.Context;
using System.Collections;

namespace E_Commerce.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataDbContext _context;
		private readonly Hashtable _repositories;

		public UnitOfWork(DataDbContext context)
		{
			_context = context;
			_repositories = new Hashtable();
		}

		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

		public async ValueTask DisposeAsync() => await _context.DisposeAsync();

		public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			var typeName = typeof(TEntity).Name;
			if (_repositories.ContainsKey(typeName)) return _repositories[typeName] as GenericRepository<TEntity, TKey>;
			var repo = new GenericRepository<TEntity, TKey>(_context);
			_repositories.Add(typeName, repo);
			return repo;
		}
	}
}
