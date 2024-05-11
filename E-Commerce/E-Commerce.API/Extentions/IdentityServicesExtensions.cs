using E_Commerce.Core.Entity.Identity;
using E_Commerce.Repository.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.API.Extentions
{
	public static class IdentityServicesExtensions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentityCore<ApplicationUser>()
				.AddEntityFrameworkStores<IdentityDataDbContext>()
				.AddSignInManager<SignInManager<ApplicationUser>>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["Token:Issuer"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
						ValidateAudience = true,
						ValidAudience = configuration["Token:Audience"],
						ValidateLifetime = true

					};
				});
			return services;
		}
	}
}
