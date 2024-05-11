using E_Commerce.Core.Entity.Identity;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface ITokenService
	{
		public string GenerateToken(ApplicationUser user);
	}
}
