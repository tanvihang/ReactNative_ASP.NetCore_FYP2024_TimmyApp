using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.UserService
{
	public interface IUserService
	{
		Task<string> Login(UserLoginDTO loginDTO);
		Task<UserT> Register(UserRegisterDTO registerDTO);
		Task<Boolean> ResetPassword(ResetPasswordDTO resetPasswordDTO);
		Task<PublicUserDTO> GetUserInfo(string userId);

	
		
	}
}
