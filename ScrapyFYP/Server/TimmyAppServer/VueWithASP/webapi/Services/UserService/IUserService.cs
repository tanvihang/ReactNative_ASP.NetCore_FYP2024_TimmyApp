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
		Task<Boolean> DeleteUser(string userId);
		Task<Boolean> ValidateUserName(string userId, string userName);
		Task<Boolean> ChangeUserInfo(ChangeUserInfoDTO changeUserInfoDTO, string userId);
		Task<Boolean> ChangeUserInfoAdmin(ChangeUserInfoDTO changeUserInfoDTO, string userId);
		Task<PageEntity<PublicUserDTO>> UserPagination(PageDTO page);
		
	}
}
