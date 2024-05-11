using E_Commerce.API.Errors;
using E_Commerce.API.Extentions;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddSwaggerService();

			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddIdentityService(builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			await InitializeData.InitializeDbAsync(app);

			app.MapControllers();

			app.UseMiddleware<CustomExceptionHandler>();

			app.Run();
		}
	}
}
