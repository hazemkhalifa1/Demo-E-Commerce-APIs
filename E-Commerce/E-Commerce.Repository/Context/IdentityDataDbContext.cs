using E_Commerce.Core.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Context
{
	public class IdentityDataDbContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityDataDbContext(DbContextOptions<IdentityDataDbContext> options) : base(options)
		{
		}
	}
}
