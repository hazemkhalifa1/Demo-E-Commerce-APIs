using System.Linq.Expressions;

namespace E_Commerce.Core.Interfaces.Specifications
{
	public interface ISpecification<T>
	{
		public Expression<Func<T, bool>> Criteria { get; }
		public List<Expression<Func<T, object>>> IncluodsExpression { get; }
		public Expression<Func<T, object>> OrderBy { get; }
		public Expression<Func<T, object>> OrderbyDesc { get; }
		public int Skip { get; }
		public int Take { get; }
		public bool IsPaginated { get; }
	}
}
