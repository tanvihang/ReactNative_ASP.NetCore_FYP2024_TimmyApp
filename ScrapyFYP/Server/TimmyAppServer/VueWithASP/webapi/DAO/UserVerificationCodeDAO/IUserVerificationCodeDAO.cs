using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.UserVerificationCodeDAO
{
	public interface IUserVerificationCodeDAO
	{
		Task<bool> SaveUserVerificationCode(SaveUserVerificationCodeDTO saveUserVerificationCodeDTO);
		Task<UserVerificationCode> GetUserVerificationCode(string? userEmail);
	}
}
