using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IUserService _userService;

		public AccountsController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<ActionResult> LogIn(LoginDto login)
		{
			var user = await _userService.LoginAsync(login);
			return user is not null ? Ok(user) : Unauthorized(new ApiResponse(401, "Incorrect Email Or Password"));
		}

		[HttpPost]
		public async Task<ActionResult> Register(RegisterDto register)
			=> Ok(await _userService.RegisterAsync(register));
	}
}
