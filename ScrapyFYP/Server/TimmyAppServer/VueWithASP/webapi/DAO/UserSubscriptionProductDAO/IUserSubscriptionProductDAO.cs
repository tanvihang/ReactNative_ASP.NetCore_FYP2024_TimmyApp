using webapi.Models;

namespace webapi.DAO.UserSubscriptionProductDAO
{
	public interface IUserSubscriptionProductDAO
	{
		Task<bool> AddUserSubscriptionProduct(UserSubscriptionProduct product);
		Task<UserSubscriptionProduct> RemoveUserSubscriptionProduct(string userSubscriptionProductId);
		Task<List<UserSubscriptionProduct>> GetUserSubscriptionProducts(string userSubscriptionProductId);
		Task<bool> AddUserSubscriptionProduct2(UserSubscriptionProduct product);
		Task<int> GetUserSubscriptionProductCount(string userSubscriptionId);
		Task<List<UserSubscriptionProduct>> GetUserSubscriptionProductsByUserSubscriptionId(string userSubscriptionId);
	}
}
