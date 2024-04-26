using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.UserVerificationCodeDAO
{
	public class UserVerificationCodeDAO : IUserVerificationCodeDAO
	{
		TimmyDbContext _context;
        public UserVerificationCodeDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }


		public async Task<UserVerificationCode> GetUserVerificationCode(string? userEmail)
		{
			var uvc = await _context.UserVerificationCodes.FirstOrDefaultAsync(uvc => uvc.UserEmail == userEmail);

			if (uvc == null)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserVerificationCodeDAO", "GetUserVerificationCode", "UserId or UserEmail not exist"));
			}
			else
			{
				return uvc;
			}
		}

		public async Task<bool> SaveUserVerificationCode(SaveUserVerificationCodeDTO saveUserVerificationCodeDTO)
		{
			// 首先检查数据库中是否已存在具有相同UserId的记录
			var existingUvc = await _context.UserVerificationCodes.FirstOrDefaultAsync(uvc => uvc.UserEmail == saveUserVerificationCodeDTO.UserEmail);

			if (existingUvc != null)
			{
				// 如果存在，则更新该记录的内容
				existingUvc.UserEmail = saveUserVerificationCodeDTO.UserEmail;
				existingUvc.VerificationCode = saveUserVerificationCodeDTO.VerificationCode;
				existingUvc.VerificationCodeExpirationDate = saveUserVerificationCodeDTO.ExpireTime;
			}
			else
			{
				// 如果不存在，则创建新的UserVerificationCode对象
				UserVerificationCode uvc = new UserVerificationCode
				{
					UserEmail = saveUserVerificationCodeDTO.UserEmail,
					VerificationCode = saveUserVerificationCodeDTO.VerificationCode,
					VerificationCodeExpirationDate = saveUserVerificationCodeDTO.ExpireTime
				};

				// 将新对象添加到数据库中
				await _context.UserVerificationCodes.AddAsync(uvc);
			}

			try
			{
				// 保存更改到数据库中
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserVerificationCodeDAO", "SaveUserVerificationCode", ex.Message));
			}
		}

	}
}
