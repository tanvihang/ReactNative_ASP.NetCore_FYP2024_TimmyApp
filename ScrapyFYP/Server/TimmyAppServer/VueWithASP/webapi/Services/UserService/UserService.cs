using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using webapi.DAO.UserTDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.JwtService;
using webapi.Services.UserVerificationCodeService;
using webapi.Utilities;
using webapi.Utilities.Enums;

namespace webapi.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly TimmyDbContext _context;

		private readonly ILogger<UserService> _logger;

		private readonly IUserTDAO _userTDao;

		private readonly IUserVerificationCodeService _userVerificationCodeService;
		private readonly IJwtService _jwtService;

		public UserService(TimmyDbContext timmyDbContext, ILogger<UserService> logger, IUserTDAO userTDAO, IUserVerificationCodeService userVerificationCodeService, IJwtService jwtService)
		{
			_context = timmyDbContext;
			_logger = logger;
			_userTDao = userTDAO;
			_userVerificationCodeService = userVerificationCodeService;
			_jwtService = jwtService;
		}

		public async Task<bool> ChangeUserInfo(ChangeUserInfoDTO changeUserInfoDTO, string userId)
		{
			// check verification
			Boolean validCode = await _userVerificationCodeService.VerifyVerificationCode(new VerifyUserVerificationCodeDTO
			{
				UserEmail = changeUserInfoDTO.email,
				VerificationCode = changeUserInfoDTO.verificationCode

			});

			if(!validCode)
			{
				throw new Exception("Verification code not correct");
			}

			// check if username exist
			string id1 = await _userTDao.CheckUserNameReturnId(userId);
			if (id1 != userId && id1 != "") {
				throw new Exception("Username Exist");
			}

			// check if useremail exist
			string id2 = await _userTDao.CheckEmailReturnId(userId);
			if(id2 != userId && id2 != "")
			{
				throw new Exception("Email Exist");
			}

			// check if phone no exist
			string id3 = await _userTDao.CheckPhoneReturnId(userId);
			if(id3 != userId && id3 != "")
			{
				throw new Exception("Phone No Exist");
			}

			// Change user info
			Boolean changed = await _userTDao.UpdateUserInfo(changeUserInfoDTO, userId);

			return changed;
		}

		public async Task<bool> ChangeUserInfoAdmin(ChangeUserInfoDTO changeUserInfoDTO, string userId)
		{

			// check if username exist
			string id1 = await _userTDao.CheckUserNameReturnId(userId);
			if (id1 != userId && id1 != "")
			{
				throw new Exception("Username Exist");
			}

			// check if useremail exist
			string id2 = await _userTDao.CheckEmailReturnId(userId);
			if (id2 != userId && id2 != "")
			{
				throw new Exception("Email Exist");
			}

			// check if phone no exist
			string id3 = await _userTDao.CheckPhoneReturnId(userId);
			if (id3 != userId && id3 != "")
			{
				throw new Exception("Phone No Exist");
			}

			// Change user info
			Boolean changed = await _userTDao.UpdateUserInfo(changeUserInfoDTO, userId);

			return changed;
		}

		public Task<bool> DeleteUser(string userId)
		{
			return _userTDao.DeleteUser(userId);
		}

		public async Task<PublicUserDTO> GetUserInfo(string userId)
		{
			PublicUserDTO publicUserDTO = await _userTDao.GetUserInfo(userId);
			
			return publicUserDTO;
		}

		public async Task<string> Login(UserLoginDTO loginDTO)
		{
			// 1. compare with database
			UserT user= await _userTDao.UserLogin(loginDTO);

			// 2. return jwt token
			if(user != null)
			{
				string jwtToken = _jwtService.GenerateJwtToken(user.UserId, user.UserName!,7);
				return jwtToken;
			}
			else
			{
				return string.Empty;
			}


			throw new NotImplementedException();
		}

		public async Task<UserT?> Register(UserRegisterDTO registerDTO)
		{
			//1. Check if user exist
			bool userNameOrEmailExist = await _userTDao.CheckEmailOrUsernameExist(registerDTO.UserName, registerDTO.UserEmail);

			if(userNameOrEmailExist)
			{
				return null;
			}

			//2. Check Verification Code
			bool validVerificationCode = await _userVerificationCodeService.VerifyVerificationCode(new VerifyUserVerificationCodeDTO
			{
				UserEmail = registerDTO.UserEmail,
				VerificationCode = registerDTO.VerificationCode
			});

			if(validVerificationCode)
			{
				UserT user = new UserT
				{
					UserId = StaticGenerator.GenerateId("U_"),
					UserName = registerDTO.UserName,
					UserEmail = registerDTO.UserEmail,
					UserPassword = registerDTO.UserPassword,
					UserLevel = 1,
					UserRegisterDate = DateTime.Now,
					UserPhoneNo = registerDTO.UserPhone
				};

				// 3. 保存用户
				await _userTDao.AddUser(user);

				return user ;
			}
			else
			{
				throw new Exception("Verification code incorrect");
			}
		}

		public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
		{
			// 1. Check Verification Code
			Boolean isValid = await _userVerificationCodeService.VerifyVerificationCode(new VerifyUserVerificationCodeDTO
			{
				UserEmail = resetPasswordDTO.UserEmail,
				VerificationCode = resetPasswordDTO.VerificationCode
			});

			if(isValid)
			{
				bool updated = await _userTDao.UserUpdatePassword(new UserUpdatePasswordDTO
				{
					UserId = await _userTDao.GetUserIdByEmail(resetPasswordDTO.UserEmail),
					NewPassword = resetPasswordDTO.NewPassword
				});

				if(updated)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			else
			{
				return false;
			}
		}

		public async Task<PageEntity<PublicUserDTO>> UserPagination(PageDTO page)
		{
			PageEntity<PublicUserDTO> pagination = await _userTDao.GetUsers(page);

			return pagination;
		}

		public async Task<bool> ValidateUserName(string userId, string userName)
		{
			PublicUserDTO userDTO = await _userTDao.GetUserInfo(userId);
			string realUserName = userDTO.UserName!;

			if(realUserName == userName) {
				return true;
			}
			else
			{
				return false;
			}			
		}
	}
}
