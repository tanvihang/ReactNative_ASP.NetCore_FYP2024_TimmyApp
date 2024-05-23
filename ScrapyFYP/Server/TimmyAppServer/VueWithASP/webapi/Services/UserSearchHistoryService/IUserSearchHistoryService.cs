using webapi.Models;

namespace webapi.Services.UserSearchHistory
{
	public interface IUserSearchHistoryService
	{
		Task<Boolean> SaveUserSearchHistory(string userId, string productFullName);
		Task<List<Models.UserSearchHistory>> GetUserSearchHistory(string userId);
		Task<Boolean> ClearUserSearchHistory(string userId);
	}
}
