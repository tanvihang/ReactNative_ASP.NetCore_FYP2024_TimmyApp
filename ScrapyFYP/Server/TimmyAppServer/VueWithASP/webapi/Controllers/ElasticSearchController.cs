using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.ElasticSearchService;
using webapi.Services.JwtService;
using webapi.Services.UserService;
using webapi.Services.UserSubscriptionService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ElasticSearchController : ControllerBase
	{
		IElasticSearchService _elasticSearchService;
		IJwtService _jwtService;
		IUserSubscriptionService _userSubscriptionService;
        public ElasticSearchController(IElasticSearchService elasticSearchService, IJwtService jwtService, IUserSubscriptionService userSubscriptionService)
        {
            _elasticSearchService = elasticSearchService;
			_jwtService = jwtService;
			_userSubscriptionService = userSubscriptionService;
        }

        [HttpPost("SearchProduct")]
		public async Task<ResponseData<PageEntity<ElasticProductDTO>>> SearchProduct(ElasticSearchPaginationDTO elasticSearchPaginationDTO, string jwtToken)
		{
			try
			{
				string userId = _jwtService.ParseJwtToUserId(jwtToken!);

				PageEntity<ElasticProductDTO> products = await _elasticSearchService.SearchProduct(elasticSearchPaginationDTO.ProductSearchTerm!, elasticSearchPaginationDTO.PageDTO!, userId);

				if(products.Count > 0)
				{
					return ResponseData<PageEntity<ElasticProductDTO>>.Success(products);
				}
				else
				{
					return ResponseData<PageEntity<ElasticProductDTO>>.Success("No product with the search term given");
				}

			}
			catch(Exception ex) 
			{
				return ResponseData<PageEntity<ElasticProductDTO>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetRandom10Product")]
		public async Task<ResponseData<PageEntity<ElasticProductDTO>>> GetRandom10Product(PageDTO pageDTO, string category)
		{
			try
			{
				PageEntity<ElasticProductDTO> products = await _elasticSearchService.GetRandom10Product(pageDTO, category);
				
				if(products.Count == 0)
				{
					return ResponseData<PageEntity<ElasticProductDTO>>.Failure("No Product");
				}

				return ResponseData<PageEntity<ElasticProductDTO>>.Success(products, "Got 10 random product");
			}
			catch(Exception ex)
			{
				return ResponseData<PageEntity<ElasticProductDTO>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetLowPriceProduct")]
		public async Task<ResponseData<List<ElasticProductDTO>>> GetLowPriceProduct(string jwtToken)
		{
			try
			{
				string userId = _jwtService.ParseJwtToUserId(jwtToken);

				UserSubscription userSubscription = await _userSubscriptionService.GetOneUserSubscription(userId);

				List<ElasticProductDTO> list = await _elasticSearchService.GetLowestPriceProduct(userSubscription);

				return ResponseData<List<ElasticProductDTO>>.Success(list);
			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticProductDTO>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetLowPriceProductForUserSubscribe")]
		public async Task<ResponseData<List<ElasticProductDTO>>> GetLowPriceProductForUserSubscribe(string userSubscriptionId)
		{
			try
			{
				UserSubscription userSubscription = await _userSubscriptionService.GetUserSubscriptionBySubscriptionId(userSubscriptionId);

				List<ElasticProductDTO> elasticProductList = await _elasticSearchService.GetLowPriceProductForUserSubscribe(userSubscription);

				return ResponseData<List<ElasticProductDTO>>.Success(elasticProductList);
			}
			catch(Exception ex)
			{
				return ResponseData<List<ElasticProductDTO>>.Failure(ex.Message);
			}

		}

		[HttpGet("GetProductOver7Days")]
		public async Task<ResponseData<List<ElasticProductDTO>>> GetProductOver7Days()
		{
			try
			{
				List<ElasticProductDTO> productOver7Days = await _elasticSearchService.GetProductOver7Days();

				if(productOver7Days.Count > 0)
				{
					return ResponseData<List<ElasticProductDTO>>.Success(productOver7Days);
				}

				return ResponseData<List<ElasticProductDTO>>.Failure("No Product over 7 days");

			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticProductDTO>>.Failure(ex.Message);
			}
		}

		[HttpGet("DeleteAllProduct")]
		public async Task<ResponseData<bool>> DeleteAllProduct()
		{
			try
			{
				bool isDeleted = await _elasticSearchService.DeleteAllProduct();

				return ResponseData<bool>.Success(isDeleted);
			}
			catch(Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

		[HttpPost("RemoveOneProduct")]
		public async Task<ResponseData<bool>> RemoveOneProduct(string uniqueId)
		{
			try
			{
				bool isDeleted = await _elasticSearchService.RemoveOneProduct(uniqueId);

				if (isDeleted)
				{
					return ResponseData<bool>.Success(isDeleted);
				}

				return ResponseData<bool>.Failure($"No item {uniqueId}");

			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

		[HttpPost("UpdateProduct")]
		public async Task<ResponseData<bool>> UpdateProduct(ElasticProductDTO elasticProductDTO)
		{
			try
			{
				bool isUpdated = await _elasticSearchService.UpdateProduct(elasticProductDTO);

				if (isUpdated)
				{
					return ResponseData<bool>.Success(isUpdated);
				}
				else
				{
					return ResponseData<bool>.Failure($"Update product {elasticProductDTO.unique_id} failed");
				}

			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

		[HttpPost("GetLowPriceProductForPriceHistory")]
		public async Task<ResponseData<List<ElasticProductDTO>>> GetLowPriceProductForPriceHistory(string category, string brand, string model)
		{
			try
			{
				List<ElasticProductDTO> products = await _elasticSearchService.GetLowPriceProductForPriceHistory(new ProductSearchTermDTO
				{
					category = category,
					brand = brand,
					model = model
				});

				if(products.Count > 0)
				{
					return ResponseData<List<ElasticProductDTO>>.Success(products);
				}

				return ResponseData<List<ElasticProductDTO>>.Failure("NO products");

			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticProductDTO>>.Failure(ex.Message);
			}
		}

		[HttpGet("GetElasticProductCategoriesCount")]
		public async Task<ResponseData<List<ElasticCategoryCountDTO>>> GetElasticProductCategoriesCount()
		{
			try
			{
				List<ElasticCategoryCountDTO> categories = await _elasticSearchService.GetElasticProductCategoriesCount();

				return ResponseData<List<ElasticCategoryCountDTO>>.Success(categories);

			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticCategoryCountDTO>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetElasticProductBrandCount")]
		public async Task<ResponseData<List<ElasticCategoryCountDTO>>> GetElasticProductBrandCount(string category)
		{
			try
			{
				List<ElasticCategoryCountDTO> categories = await _elasticSearchService.GetElasticProductBrandCount(category);

				return ResponseData<List<ElasticCategoryCountDTO>>.Success(categories);

			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticCategoryCountDTO>>.Failure(ex.Message);
			}
		}

		[HttpPost("GetElasticProductModelCount")]
		public async Task<ResponseData<ElasticSearchModelDTO>> GetElasticProductModelCount(string category, string brand)
		{
			try
			{
				ElasticSearchModelDTO categories = await _elasticSearchService.GetElasticProductModelCount(category, brand);

				return ResponseData<ElasticSearchModelDTO>.Success(categories);

			}
			catch (Exception ex)
			{
				return ResponseData<ElasticSearchModelDTO>.Failure(ex.Message);
			}
		}
	}
}
