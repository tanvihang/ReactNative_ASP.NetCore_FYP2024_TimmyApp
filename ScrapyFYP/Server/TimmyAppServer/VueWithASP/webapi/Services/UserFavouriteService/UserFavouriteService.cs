using webapi.DAO.UserFavouriteDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.ElasticSearchService;
using webapi.Utilities;

namespace webapi.Services.UserFavouriteService
{
	public class UserFavouriteService : IUserFavouriteService
	{
		private readonly IUserFavouriteDAO _userFavouriteDAO;
		private readonly IElasticSearchService _elasticSearchService;

        public UserFavouriteService(IUserFavouriteDAO userFavouriteDAO, IElasticSearchService elasticSearchService)
        {
            _userFavouriteDAO = userFavouriteDAO;
			_elasticSearchService = elasticSearchService;
        }

        public async Task<bool> FavouriteProduct(string userId, string productUniqueId)
		{
			bool isAdded = await _userFavouriteDAO.FavouriteProduct(userId, productUniqueId);

			return isAdded;
		}

		public async Task<List<ElasticProductDTO>> GetUserFavourite(string userId)
		{
			// 1. Get user favourite product unique id lists
			List<string> userFavouriteProductIds = await _userFavouriteDAO.GetUserFavouriteProductList(userId);

			if(userFavouriteProductIds == null)
			{
				return new List<ElasticProductDTO>();
			}

			try
			{
				// 2. Retrieve product from es
				List<ElasticProductDTO> result = await _elasticSearchService.GetUserFavourite(userFavouriteProductIds);

				return result;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("UserFavouriteService", "GetUserFavourite", ex.Message));
			}

			
		}

		public async Task<UserFavourite> UnFavouriteProduct(string userId, string productUniqueId)
		{
			UserFavourite userFavourite = await _userFavouriteDAO.UnFavouriteProduct(userId, productUniqueId);

			return userFavourite;
		}
	}
}
