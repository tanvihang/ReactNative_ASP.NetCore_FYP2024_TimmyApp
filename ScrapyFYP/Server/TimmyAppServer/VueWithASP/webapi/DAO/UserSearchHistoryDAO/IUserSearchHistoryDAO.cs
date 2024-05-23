using webapi.Models;

namespace webapi.DAO.UserSearchHistoryDAO
{
	public interface IUserSearchHistoryDAO
	{
		Task<bool> SaveUserSearchHistory(UserSearchHistory userSearchHistory);
		Task<List<UserSearchHistory>> GetUserSearchHistory(string userId);
		Task<bool> ClearUserSearchHistory(string userId);

	}
}
