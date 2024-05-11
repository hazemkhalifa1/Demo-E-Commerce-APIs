using E_Commerce.Core.Entity;
using E_Commerce.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Specifications
{
	public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
		{
			var query = inputQuery;
			if (specification.Criteria is not null)
				query = query.Where(specification.Criteria);

			if (specification.IsPaginated)
				query = query.Skip(specification.Skip).Take(specification.Take);

			if (specification.IncluodsExpression.Any())
				query = specification.IncluodsExpression
					.Aggregate(query, (CurrentQuery, expression) => CurrentQuery.Include(expression));

			if (specification.OrderBy is not null)
				query = query.OrderBy(specification.OrderBy);

			if (specification.OrderbyDesc is not null)
				query = query.OrderByDescending(specification.OrderbyDesc);

			return query;
		}
	}
}
