using webapi.Models.DTO;

namespace webapi.Services.UserVerificationCodeService
{
	public interface IUserVerificationCodeService
	{
		Task<Boolean> GenerateVerificationCode(GenerateUserVerificationCodeDTO generateUserVerificationCodeDTO);

		Task<Boolean> VerifyVerificationCode(VerifyUserVerificationCodeDTO verifyUserVerificationCodeDTO);
	}
}
