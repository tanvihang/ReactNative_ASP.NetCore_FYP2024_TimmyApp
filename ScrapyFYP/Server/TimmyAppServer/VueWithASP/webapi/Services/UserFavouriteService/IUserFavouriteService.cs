using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.UserFavouriteService
{
	public interface IUserFavouriteService
	{
		Task<Boolean> FavouriteProduct(string userId, string productUniqueId);
		Task<UserFavourite> UnFavouriteProduct(string userId, string productUniqueId);
		Task<List<ElasticProductDTO>> GetUserFavourite(string userId);
		Task<List<string>> GetUserFavouriteIdList(string userId);
	}
}
