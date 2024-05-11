using E_Commerce.Core.Entity.Identity;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services
{
	public class TokenService : ITokenService
	{
		//private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		//public TokenService(UserManager<ApplicationUser> userManager)
		//{
		//	_userManager = userManager;
		//}

		public string GenerateToken(ApplicationUser user)
		{
			var Claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, user.UserName)
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(Claims),
				Issuer = _configuration["Token:Issuer"],
				Audience = _configuration["Token:Audience"],
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
			//var roles = await _userManager.GetRolesAsync(user);
			//Claims.AddRange(roles.Select(r=>new Claim(ClaimTypes.Role,r)));
		}
	}
}
