using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entity.Identity;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signinManager;
		private readonly ITokenService _tokenService;

		public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signinManager = signinManager;
			_tokenService = tokenService;
		}

		public async Task<UserDto?> LoginAsync(LoginDto login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email);
			if (user is not null)
			{
				var result = await _signinManager.CheckPasswordSignInAsync(user, login.Password, false);
				if (result.Succeeded)
				{
					return new UserDto()
					{
						DisplayName = user.DisplayName,
						Email = user.Email,
						Token = _tokenService.GenerateToken(user)
					};
				}
			}
			return null;
		}


		public async Task<UserDto> RegisterAsync(RegisterDto register)
		{
			var user = await _userManager.FindByEmailAsync(register.Email);
			if (user is not null) throw new Exception("Email Exists");
			var AppUser = new ApplicationUser
			{
				DisplayName = register.DisplayName,
				Email = register.Email,
				UserName = register.DisplayName.Trim()
			};
			var result = await _userManager.CreateAsync(AppUser, register.Password);
			if (result.Succeeded) throw new Exception(result.Errors.SelectMany(e => e.Description).ToString());
			return new UserDto
			{
				DisplayName = AppUser.DisplayName,
				Email = AppUser.Email,
				Token = _tokenService.GenerateToken(AppUser)
			};
		}
	}
}
