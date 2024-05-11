using E_Commerce.Core.Interfaces.Specifications;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Specifications
{
	public class BaseSpecification<T> : ISpecification<T>
	{
		public Expression<Func<T, bool>> Criteria { get; }

		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public List<Expression<Func<T, object>>> IncluodsExpression { get; } = new();

		public Expression<Func<T, object>> OrderBy { get; protected set; }

		public Expression<Func<T, object>> OrderbyDesc { get; protected set; }

		public int Skip { get; protected set; }

		public int Take { get; protected set; }

		public bool IsPaginated { get; set; }

		public void ApplyPaginated(int PageSize, int PageIndex)
		{
			IsPaginated = true;
			Skip = (PageIndex - 1) * PageSize;
			Take = PageSize;
		}
	}
}
