using E_Commerce.Core.Entity.Identity;
using E_Commerce.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extentions
{
	public static class InitializeData
	{
		public static async Task InitializeDbAsync(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var service = scope.ServiceProvider;
				var loggerFactory = service.GetService<ILoggerFactory>();
				try
				{
					var context = service.GetRequiredService<DataDbContext>();
					var usermanager = service.GetRequiredService<UserManager<ApplicationUser>>();
					if ((await context.Database.GetPendingMigrationsAsync()).Any())
					{
						await context.Database.MigrateAsync();
					}
					await DataContextSeed.SeddingDataAsync(context);
					await IdentityDataDbContextSeed.SeddingDataAsync(usermanager);
				}
				catch (Exception ex)
				{
					var logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex.Message);
				}
			}
		}
	}
}
