using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.UserSubscriptionDAO
{
	public interface IUserSubscriptionDAO
	{
		Task<bool> AddUserSubscription(UserSubscription userSubscription);
		Task<UserSubscription> RemoveUserSubscription(string userId, string productFullName);
		Task<List<UserSubscription>> GetUserSubscription(string userId);
		Task<UserSubscription> GetOneUserSubscription(string userId);
		Task<UserSubscription> GetUserSubscriptionBySubscriptionId(string userSubscriptionId);
		Task<List<UserSubscription>> GetUserSubscriptionByAboveUserLevel(int level);
		Task<List<UserSubscription>> GetUserSubscriptionByNotificationTime(int time);
	}
}
