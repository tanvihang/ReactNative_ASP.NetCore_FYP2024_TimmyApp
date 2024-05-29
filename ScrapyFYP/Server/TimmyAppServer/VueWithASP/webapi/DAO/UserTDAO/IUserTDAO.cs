using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.UserTDAO
{
	public interface IUserTDAO
	{
		Task<PublicUserDTO> GetUserInfo(string userId);
		Task<UserT> UserLogin(UserLoginDTO loginDTO);
		Task<List<PublicUserDTO>> GetAllUsers();
		Task<int> UserUpgrade(string userId);
		Task<Boolean> UserUpdatePassword(UserUpdatePasswordDTO userUpdatePasswordDTO);
		Task<Boolean> DeleteUser(string userId);
		Task<Boolean> AddUser(UserT userT);
		Task<PageEntity<PublicUserDTO>> GetUsers(PageDTO pageDTO);
		Task<Boolean> CheckEmailOrUsernameExist(string userName, string userEmail);
		Task<string> GetUserIdByEmail(string email);
		Task<string> CheckUserNameReturnId(string userName);
		Task<string> CheckEmailReturnId(string email);
		Task<string> CheckPhoneReturnId(string phone);
		Task<Boolean> UpdateUserInfo(ChangeUserInfoDTO changeUserInfoDTO, string userId);
	}
}
