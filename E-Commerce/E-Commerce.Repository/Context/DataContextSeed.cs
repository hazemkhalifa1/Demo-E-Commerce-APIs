using E_Commerce.Core.Entity;
using E_Commerce.Core.Entity.OrderEntities;
using System.Text.Json;

namespace E_Commerce.Repository.Context
{
	public class DataContextSeed
	{
		public static async Task SeddingDataAsync(DataDbContext context)
		{
			if (!context.Set<ProductType>().Any())
			{
				var TypeData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
				if (Types.Any() && Types is not null)
				{
					await context.Set<ProductType>().AddRangeAsync(Types);
					await context.SaveChangesAsync();
				}
			}
			if (!context.Set<ProductBrand>().Any())
			{
				var BrandData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\brands.json");
				var Brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
				if (Brand.Any() && Brand is not null)
				{
					await context.Set<ProductBrand>().AddRangeAsync(Brand);
					await context.SaveChangesAsync();
				}
			}
			if (!context.Set<Product>().Any())
			{
				var ProductData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\products.json");
				var Product = JsonSerializer.Deserialize<List<Product>>(ProductData);
				if (Product.Any() && Product is not null)
				{
					await context.Set<Product>().AddRangeAsync(Product);
					await context.SaveChangesAsync();
				}
			}
			if (!context.Set<DeliveryMethod>().Any())
			{
				var DeliveryData = await File.ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\delivery.json");
				var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
				if (Delivery.Any() && Delivery is not null)
				{
					await context.Set<DeliveryMethod>().AddRangeAsync(Delivery);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}
