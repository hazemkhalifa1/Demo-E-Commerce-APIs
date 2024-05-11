using E_Commerce.Core.Entity.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Context.Configration
{
	internal class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(o => o.OIProduct, o => o.WithOwner());

			builder.Property(o => o.Price).HasColumnType("decimal(18,3)");
		}
	}
}
