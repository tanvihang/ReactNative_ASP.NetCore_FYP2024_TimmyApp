using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.UserSubscriptionService
{
	public interface IUserSubscriptionService
	{
		//TODO comment IUserSubscriptionService
		//Task<UserSubscription> SetUserSubscription(UserPublic user, string productName);

		//Task<Boolean> CheckUserSubscriptionLimit(string userid);

		//Task<UserSubscription> RemoveUserSubscription(string userid, string productName);

		Task<UserSubscription> AddUserSubscription(AddUserSubscriptionDTO addUserSubscriptionDTO, string userId);
		Task<UserSubscription> RemoveUserSubscription(RemoveUserSubscriptionDTO removeUserSubscriptionDTO, string userId);
		Task<List<UserSubscription>> GetUserSubscriptions(string userId);
		Task<UserSubscription> GetOneUserSubscription(string userId);
		Task<UserSubscription> GetUserSubscriptionBySubscriptionId(string userSubscriptionId);
		Task<List<UserSubscription>> GetUserSubscriptionByAboveUserLevel(int level);
		Task<List<UserSubscription>> GetUserSubscriptionByNotificationTime(int time);

	}
}
