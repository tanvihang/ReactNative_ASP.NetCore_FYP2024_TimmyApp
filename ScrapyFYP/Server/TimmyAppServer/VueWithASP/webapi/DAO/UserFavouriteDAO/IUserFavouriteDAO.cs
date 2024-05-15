using webapi.Models;
using webapi.Models.DTO;

namespace webapi.DAO.UserFavouriteDAO
{
	public interface IUserFavouriteDAO
	{
		Task<Boolean> FavouriteProduct(string userId, string productUniqueId);
		Task<UserFavourite> UnFavouriteProduct(string userId, string productUniqueId);
		Task<List<string>> GetUserFavouriteProductList(string userId);

	}
}
