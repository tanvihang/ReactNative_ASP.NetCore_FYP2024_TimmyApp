using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.UserTDAO
{
	public class UserTDAO : IUserTDAO
	{
		TimmyDbContext _context;
        public UserTDAO(TimmyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(UserT userT)
		{
			var existUser = _context.UserTs.FirstOrDefault(u => u.UserName == userT.UserName || u.UserEmail == userT.UserEmail);
			
			if(existUser == null)
			{
				await _context.UserTs.AddAsync(userT);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "AddUser", "User email or name exist"));
			}
		}

		public async Task<bool> DeleteUser(string userId)
		{
			var existUser = _context.UserTs.FirstOrDefault(u => u.UserId == userId);

			if (existUser != null)
			{
				_context.UserTs.Remove(existUser);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "DeleteUser", "User does not exist"));
			}
		}

		public async Task<PublicUserDTO> GetUserInfo(string userId)
		{
			var existUser = await _context.UserTs.FirstOrDefaultAsync(u => u.UserId == userId);

			if (existUser != null)
			{
				return DTOFactory.ConvertToPublicUserDTO(existUser);
			}
			else
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "GetUserInfo", "User not exist"));
			}
		}

		public async Task<List<PublicUserDTO>> GetAllUsers()
		{
			var users = await _context.UserTs.ToListAsync();

			if(users.Count == 0)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "GetAllUsers", "No Users"));
			}
			else
			{
				return users.Select(user => DTOFactory.ConvertToPublicUserDTO(user)).ToList();
			}

		}

		public async Task<UserT> UserLogin(UserLoginDTO loginDTO)
		{
			var user = await _context.UserTs.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserToken || u.UserEmail == loginDTO.UserToken);

			if(user != null)
			{
				if(user.UserPassword == loginDTO.UserPassword)
				{
					return user;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "UserLogin", "Password Incorrect"));
				}
			}
			else
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "User Login", "No User"));
			}
			
		}

		public async Task<bool> UserUpdatePassword(UserUpdatePasswordDTO userUpdatePasswordDTO)
		{
			UserT user = await _context.UserTs.FirstOrDefaultAsync(ut => ut.UserId == userUpdatePasswordDTO.UserId);

			if(user != null)
			{
				user.UserPassword = userUpdatePasswordDTO.NewPassword;

				try
				{
					await _context.SaveChangesAsync();
					return true;
				}
				catch(Exception ex)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "UserUpdatePassword", ex.Message));
				}
			}
			else
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "UserUpdatePassword", "No User"));
			}

		}

		public async Task<int> UserUpgrade(string userId)
		{
			throw new NotImplementedException();
		}

		public async Task<PageEntity<PublicUserDTO>> GetUsers(PageDTO pageDTO)
		{
			try
			{
				// page starts from 1
				List<UserT> userList = await _context.UserTs.Skip((pageDTO.CurrentPage -1 ) * pageDTO.PageSize).Take(pageDTO.PageSize).ToListAsync();

				PageEntity<PublicUserDTO> userPages = new PageEntity<PublicUserDTO>();

				userPages.Count = userList.Count;
				userPages.rows = new List<PublicUserDTO>();

				foreach (UserT user in userList)
				{
					PublicUserDTO userDTO = DTOFactory.ConvertToPublicUserDTO(user);
                    userPages.rows.Add(userDTO);
				}

				return userPages;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "GetUsers", ex.Message));
			}
		}

		public async Task<bool> CheckEmailOrUsernameExist(string userName, string userEmail)
		{
			try
			{
				var user = await _context.UserTs.FirstOrDefaultAsync(ut => ut.UserName == userName || ut.UserEmail == userEmail);

				if (user != null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(Exception ex) 
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "CheckEmailOrUsernameExist", ex.Message));
			}

		}

		public async Task<string> GetUserIdByEmail(string email)
		{
			var user = await _context.UserTs.FirstOrDefaultAsync(ut => ut.UserEmail == email);
			
			if(user != null)
			{
				return user.UserId;
			}
			else
			{
                //throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "GetUserIdByEmail", "Email not exist"));
                await Console.Out.WriteLineAsync(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "GetUserIdByEmail", "Email not exist"));
				return null;
			}
		}

		public async Task<string> CheckUserNameReturnId(string userName)
		{
			UserT user= await _context.UserTs.FirstOrDefaultAsync(us => us.UserName == userName);

			if(user != null)
			{
				return user.UserId;
			}
			else
			{
				return "";
			}
		}

		public async Task<string> CheckEmailReturnId(string email)
		{
			UserT user = await _context.UserTs.FirstOrDefaultAsync(us => us.UserEmail == email);

			if (user != null)
			{
				return user.UserId;
			}
			else
			{
				return "";
			}
		}

		public async Task<string> CheckPhoneReturnId(string phone)
		{
			UserT user = await _context.UserTs.FirstOrDefaultAsync(us => us.UserPhoneNo== phone);

			if (user != null)
			{
				return user.UserId;
			}
			else
			{
				return "";
			}
		}

		public async Task<bool> UpdateUserInfo(ChangeUserInfoDTO changeUserInfoDTO, string userId)
		{
			try
			{
				UserT user = await _context.UserTs.FirstOrDefaultAsync(us => us.UserId == userId);
				UserT newUser = user;

				if (user != null)
				{
					user.UserEmail = changeUserInfoDTO.newUserEmail;
					user.UserPhoneNo = changeUserInfoDTO.newPhoneNo;
					user.UserName = changeUserInfoDTO.newUserName;
				}

				_context.UserTs.Update(user);
				await _context.SaveChangesAsync();

				return true;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserTDAO", "UpdateUserInfo", ex.Message));
			}
		}
	}
}
