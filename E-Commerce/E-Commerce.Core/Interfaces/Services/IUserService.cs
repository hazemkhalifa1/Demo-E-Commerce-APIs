using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IUserService
	{
		public Task<UserDto> LoginAsync(LoginDto login);
		public Task<UserDto> RegisterAsync(RegisterDto register);

	}
}
