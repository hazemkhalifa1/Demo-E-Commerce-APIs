using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Core.Entity.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string DisplayName { get; set; }
		public Address Address { get; set; }
	}
}
