using webapi.DAO.UserSearchHistoryDAO;
using webapi.Models;
using webapi.Services.UserSearchHistory;
using webapi.Utilities;

namespace webapi.Services.UserSearchHistoryService
{
	public class UserSearchHistoryService : IUserSearchHistoryService
	{
		IUserSearchHistoryDAO _userSearchHistoryDAO;
        public UserSearchHistoryService(IUserSearchHistoryDAO userSearchHistoryDAO)
        {
            _userSearchHistoryDAO = userSearchHistoryDAO;
        }
        public async Task<List<Models.UserSearchHistory>> GetUserSearchHistory(string userId)
		{
			return await _userSearchHistoryDAO.GetUserSearchHistory(userId);
		}

		public async Task<bool> SaveUserSearchHistory(string userId, string productFullName)
		{
			try
			{


				Models.UserSearchHistory userSearchHistory = new Models.UserSearchHistory
				{
					UserId = userId,
					UserSearchHistoryProductFullName = productFullName,
					UserSearhHistoryDate = DateTime.Now
				};

				await _userSearchHistoryDAO.SaveUserSearchHistory(userSearchHistory);

				return true;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("UserSearchHistoryService", "SaveUserSearchHistory", ex.Message));
			}
			
		}
	}
}
