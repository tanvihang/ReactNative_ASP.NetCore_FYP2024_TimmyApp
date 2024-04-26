using webapi.DAO.UserTDAO;
using webapi.DAO.UserVerificationCodeDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.NotificationService;
using webapi.Utilities;

namespace webapi.Services.UserVerificationCodeService
{
	public class UserVerificationCodeService : IUserVerificationCodeService
	{
		IUserVerificationCodeDAO _userVerificationCodeDAO;
		IUserTDAO _userTDAO;

		INotificationService _notificationService;
        public UserVerificationCodeService(IUserVerificationCodeDAO userVerificationCodeDAO, IUserTDAO userTDAO, INotificationService notificationService)
        {
            _userVerificationCodeDAO = userVerificationCodeDAO;
			_userTDAO = userTDAO;

			_notificationService = notificationService;
        }
        public async Task<bool> GenerateVerificationCode(GenerateUserVerificationCodeDTO generateUserVerificationCodeDTO)
		{
			// Check the email 格式
			bool isValidEmail =  StaticGenerator.IsValidEmail(generateUserVerificationCodeDTO.Email);

			if(isValidEmail == false)
			{
				return false;
			}

			string verificationCode = Guid.NewGuid().ToString().Substring(0, 4);

			SaveUserVerificationCodeDTO saveUserVerificationCodeDTO = new SaveUserVerificationCodeDTO
			{
				VerificationCode = verificationCode,
				UserEmail = generateUserVerificationCodeDTO.Email!,
				ExpireTime = DateTime.Now.AddDays(1)

			};

			// 保存验证码
			await _userVerificationCodeDAO.SaveUserVerificationCode(saveUserVerificationCodeDTO);

			// 发送邮箱
			await _notificationService.SendOneMail(new SendMailDTO
			{
				title = "Your registration token",
				content = verificationCode,
				type = Utilities.Enums.NotificationTypeEnum.email.ToString()

			}, generateUserVerificationCodeDTO.Email!);


			return true;
		}

		public async Task<bool> VerifyVerificationCode(VerifyUserVerificationCodeDTO verifyUserVerificationCodeDTO)
		{
			UserVerificationCode uvc= await _userVerificationCodeDAO.GetUserVerificationCode(verifyUserVerificationCodeDTO.UserEmail);
			DateTime now = DateTime.Now;

			if(uvc != null && uvc.VerificationCode == verifyUserVerificationCodeDTO.VerificationCode && DateTime.Now < uvc.VerificationCodeExpirationDate?.AddDays(1))
			{
				return true;
			}
			else
			{
				return false;
			}

		}
	}
}
