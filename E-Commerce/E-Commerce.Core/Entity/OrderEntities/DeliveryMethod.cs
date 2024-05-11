﻿namespace E_Commerce.Core.Entity.OrderEntities
{
	public class DeliveryMethod : BaseEntity<int>
	{
		public string ShortName { get; set; }
		public string Description { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Price { get; set; }
	}
}