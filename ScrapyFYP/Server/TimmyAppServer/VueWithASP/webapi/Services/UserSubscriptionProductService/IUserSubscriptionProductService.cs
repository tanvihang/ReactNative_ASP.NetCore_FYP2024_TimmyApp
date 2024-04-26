using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.UserSubscriptionProductService
{
	public interface IUserSubscriptionProductService
	{
		Task<UserSubscriptionProduct> AddUserSubscriptionProduct(AddUserSubscriptionProductDTO addUserSubscriptionProductDTO);
		Task<UserSubscriptionProduct> RemoveUserSubscriptionProduct(string userSubscriptionProductId);
		Task<List<UserSubscriptionProduct>> GetUserSubscriptionProductsByUserSubscriptionId(string userSubscriptionId);
	}
}
