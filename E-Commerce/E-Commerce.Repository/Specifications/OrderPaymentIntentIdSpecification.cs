using E_Commerce.Core.Entity.OrderEntities;

namespace E_Commerce.Repository.Specifications
{
	public class OrderPaymentIntentIdSpecification : BaseSpecification<Order>
	{
		public OrderPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
		{
			IncluodsExpression.Add(o => o.DeliveryMethod);
		}
	}
}
