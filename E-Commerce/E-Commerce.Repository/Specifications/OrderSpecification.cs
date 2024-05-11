using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.Repository.Specifications
{
	public class OrderSpecification : BaseSpecification<Order>
	{
		public OrderSpecification(string email)
			: base(o => o.BuyerEmail == email)
		{
			IncluodsExpression.Add(o => o.DeliveryMethod);
			IncluodsExpression.Add(o => o.OrderItems);

			OrderBy = o => o.OrderDate;
		}
		public OrderSpecification(Guid id, string email)
			: base(o => o.Id == id && o.BuyerEmail == email)
		{
			IncluodsExpression.Add(o => o.DeliveryMethod);
			IncluodsExpression.Add(o => o.OrderItems);

		}
	}
}
