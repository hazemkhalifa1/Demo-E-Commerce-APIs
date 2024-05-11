using E_Commerce.API.Errors;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API.Extentions
{
	public static class ApplicationServicesExtentions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IConnectionMultiplexer>(option =>
			{
				var config = ConfigurationOptions.
				Parse(configuration.GetConnectionString("RedisConnection"));
				return ConnectionMultiplexer.Connect(config);
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState.Where(m => m.Value.Errors.Any()).
					SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();

					return new BadRequestObjectResult(new ApiValidationResponse() { Errors = errors });
				};
			});

			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IBasketRepository, BasketRepositry>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<ICashService, CashService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserService>();
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddDbContext<IdentityDataDbContext>(o =>
			{
				o.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnection"));
			});

			services.AddDbContext<DataDbContext>(o =>
			{
				o.UseSqlServer(configuration.GetConnectionString("SQLConnection"));
			});
			return services;
		}
	}
}
