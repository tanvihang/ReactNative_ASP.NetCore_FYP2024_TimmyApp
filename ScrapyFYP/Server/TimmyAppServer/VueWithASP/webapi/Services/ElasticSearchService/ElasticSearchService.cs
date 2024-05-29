using Microsoft.OpenApi.Services;
using Nest;
using webapi.DAO.ElasticSearchDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.UserSearchHistory;
using webapi.Services.UserService;
using webapi.Utilities;

namespace webapi.Services.ElasticSearchService
{
	public class ElasticSearchService : IElasticSearchService
	{
		private readonly IElasticSearchDAO _elasticSearchDAO;
		private readonly IUserSearchHistoryService _userSearchHistoryService;

		public ElasticSearchService(IElasticSearchDAO elasticSearchDAO, IUserSearchHistoryService userSearchHistoryService)
		{
			_elasticSearchDAO = elasticSearchDAO;
			_userSearchHistoryService = userSearchHistoryService;
		}

		public async Task<bool> DeleteAllProduct()
		{
			bool isDeleted = await _elasticSearchDAO.DeleteAllProduct();
			return isDeleted;
		}

		public async Task<List<ElasticCategoryCountDTO>> GetElasticProductBrandCount(string category)
		{
			return await _elasticSearchDAO.GetElasticProductBrandCount(category);
		}

		public async Task<List<ElasticCategoryCountDTO>> GetElasticProductCategoriesCount()
		{
			return await _elasticSearchDAO.GetElasticProductCategoriesCount();
		}

		public async Task<ElasticSearchModelDTO> GetElasticProductModelCount(string category, string model)
		{
			return await _elasticSearchDAO.GetElasticProductModelCount(category, model);
		}

		public async Task<List<ElasticProductDTO>> GetLowestPriceProduct(UserSubscription userSubscription)
		{
			// 1. Get Product
			List<ElasticProductDTO> list = await _elasticSearchDAO.GetLowPriceElasticProduct(new ProductSearchTermDTO
			{
				category = userSubscription.UserSubscriptionProductCategory,
				brand = userSubscription.UserSubscriptionProductBrand,
				model = userSubscription.UserSubscriptionProductModel,
				description = userSubscription.UserSubscriptionProductDescription,
				highest_price = userSubscription.UserSubscriptionProductHighestPrice,
				lowest_price = userSubscription.UserSubscriptionProductLowestPrice,
				isTest = 0,
				sort = "priceasc"
			});

			if(list.Count == 0)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetLowestPriceProduct", $"No valid product: {userSubscription.UserSubscriptionProductModel}"));
			}

			// 2. Clean Product

			// 3. Save Product To UserSubscriptionProduct

			return list;

		}

		public async Task<List<ElasticProductDTO>> GetLowPriceProductForPriceHistory(ProductSearchTermDTO productSearchTermDTO)
		{

			List<ElasticProductDTO> elasticProductDTOs = await _elasticSearchDAO.GetLowPriceProductForPriceHistory(productSearchTermDTO);

			return elasticProductDTOs;
		}

		public async Task<List<ElasticProductDTO>> GetLowPriceProductForUserSubscribe(UserSubscription userSubscription)
		{
			//1. 获取最多10个数据
			List<ElasticProductDTO> elasticProductDTOs = await _elasticSearchDAO.GetLowPriceProductForUserSubscribe(userSubscription);

			//2. 进行过滤，挑选出好的几个加入列表


			return elasticProductDTOs;
		}

		public async Task<ElasticProductDTO> GetOneLowestPriceProduct(ProductSearchTermDTO productSearchTermDTO)
		{
			ElasticProductDTO product = await _elasticSearchDAO.GetOneLowestPriceProduct(productSearchTermDTO);

			return product;
		}

		public async Task<List<ElasticProductDTO>> GetProductOver7Days()
		{
			List<ElasticProductDTO> productList = await _elasticSearchDAO.GetProductOver7Days();

			return productList;
		}

		public async Task<PageEntity<ElasticProductDTO>> GetRandom10Product(PageDTO pageDTO, string category)
		{
			try
			{
				return await _elasticSearchDAO.GetRandom10Product(pageDTO, category);
            }
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "GetRandom10Product", ex.Message));
			}
		}

		public async Task<List<ElasticProductDTO>> GetUserFavourite(List<string> productUniqueId)
		{
			List<ElasticProductDTO> list = await _elasticSearchDAO.GetUserFavourite(productUniqueId);

			return list;
		}

		public async Task<bool> RemoveOneProduct(string unique_id)
		{
			return await _elasticSearchDAO.RemoveOneProduct(unique_id);
		}

		public async Task<PageEntity<ElasticProductDTO>> SearchProduct(ProductSearchTermDTO searchTermDTO, PageDTO pageDTO, string? userId)
		{
            try
			{
				List<ElasticProductDTO> productList = await _elasticSearchDAO.GetPaginationElasticProduct(searchTermDTO, pageDTO);

				PageEntity<ElasticProductDTO> pageEntity = new PageEntity<ElasticProductDTO>
				{
					Count = productList.Count,
					rows = productList
				};

				if(userId != null)
				{
					bool isAdded = await _userSearchHistoryService.SaveUserSearchHistory(userId, StaticGenerator.GenerateProductFullName(searchTermDTO.category!, searchTermDTO.brand!, searchTermDTO.model!));
				}

				return pageEntity;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ElasticSearchService", "SearchProduct", ex.Message));
			}

		}

		public async Task<bool> UpdateProduct(ElasticProductDTO elasticProductDTO)
		{
			return await _elasticSearchDAO.UpdateProduct(elasticProductDTO);
		}

	}

}
