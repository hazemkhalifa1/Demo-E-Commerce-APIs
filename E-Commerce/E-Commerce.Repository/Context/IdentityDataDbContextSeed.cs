using E_Commerce.Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository.Context
{
	public class IdentityDataDbContextSeed
	{
		public static async Task SeddingDataAsync(UserManager<ApplicationUser> _userManager)
		{
			if (!_userManager.Users.Any())
			{
				var user = new ApplicationUser
				{
					Address = new Address
					{
						City = "Cairo",
						Street = "Elgach",
						State = "Zifta",
						Country = "Egypt",
						PostalCode = "12345"
					},
					DisplayName = "Hazem Khalifa",
					Email = "hazemkhalifa1250@gmail.com",
					UserName = "hazemkhalifa"
				};

				await _userManager.CreateAsync(user, "P@ssw0rd");
			}
		}
	}
}
