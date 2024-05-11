using E_Commerce.Core.Entity;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Specifications;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Repositories
{
	internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly DataDbContext _context;

		public GenericRepository(DataDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

		public async Task DeleteAsync(TKey id) => _context.Set<TEntity>().Remove(await GetAsync(id));

		public async Task<IReadOnlyList<TEntity>> GetAllSpecAsync(ISpecification<TEntity> specification)
		=> await ApplySpecification(specification).ToListAsync();


		public async Task<TEntity> GetAsync(TKey id) => (await _context.Set<TEntity>().FindAsync(id))!;

		public async Task<int> GetProductCountSpscAsync(ISpecification<TEntity> specification)
		=> await ApplySpecification(specification).CountAsync();

		public async Task<TEntity> GetSpecAsync(ISpecification<TEntity> specification)
		=> (await ApplySpecification(specification).FirstOrDefaultAsync())!;

		public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

		private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
		=> SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification);

	}
}
