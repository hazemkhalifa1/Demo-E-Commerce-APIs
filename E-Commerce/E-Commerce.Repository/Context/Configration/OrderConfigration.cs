using E_Commerce.Core.Entity.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Repository.Context.Configration
{
	internal class OrderConfigration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasMany(order => order.OrderItems).WithOne()
				.OnDelete(DeleteBehavior.Cascade);


			builder.OwnsOne(order => order.ShoppinAddress, o => o.WithOwner());

			builder.Property(order => order.SubTotal).HasColumnType("decimal(18,5)");
		}
	}
}
