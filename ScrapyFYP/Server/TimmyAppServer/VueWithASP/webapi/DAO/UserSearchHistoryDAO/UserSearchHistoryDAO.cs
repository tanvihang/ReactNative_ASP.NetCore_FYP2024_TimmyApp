using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Services.UserService;
using webapi.Utilities;

namespace webapi.DAO.UserSearchHistoryDAO
{
	public class UserSearchHistoryDAO : IUserSearchHistoryDAO
	{
		TimmyDbContext _context;
        public UserSearchHistoryDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }

		public async Task<bool> ClearUserSearchHistory(string userId)
		{
			try
			{
				List<UserSearchHistory> list = await _context.UserSearchHistories.Where(ush => ush.UserId == userId).ToListAsync();

                await Console.Out.WriteLineAsync(list.Count().ToString());

                if (list.Count > 0)
				{
					_context.RemoveRange(list);
					await _context.SaveChangesAsync();
					return true;

				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSearchHistoryDAO", "ClearUserSearchHistory", "User has no search history!"));
				}

			}catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSearchHistoryDAO", "ClearUserSearchHistory", ex.Message));

			}

		}

		public async Task<List<UserSearchHistory>> GetUserSearchHistory(string userId)
		{
			try
			{
				List<UserSearchHistory> list = await _context.UserSearchHistories.Where(ush => ush.UserId == userId).ToListAsync();

				if(list.Count == 0)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSearchHistoryDAO", "GetUserSearchHistory", "user no search history"));
				}

				return list;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSearchHistoryDAO", "GetUserSearchHistory", "Get user search history failed"));
			}
		}

		public async Task<bool> SaveUserSearchHistory(UserSearchHistory userSearchHistory)
		{
			try
			{
				UserSearchHistory ush = await _context.UserSearchHistories.FirstOrDefaultAsync(ush => ush.UserSearchHistoryProductFullName == userSearchHistory.UserSearchHistoryProductFullName && ush.UserId == userSearchHistory.UserId);

				if(ush != null)
				{
					ush.UserSearhHistoryDate = userSearchHistory.UserSearhHistoryDate;
					await _context.SaveChangesAsync();
				}
				else
				{
					await _context.UserSearchHistories.AddAsync(userSearchHistory);
					await _context.SaveChangesAsync();
				}

				return true;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSearchHistoryDAO", "SaveUserSearchHistory", ex.Message));
			}

		}
	}
}
