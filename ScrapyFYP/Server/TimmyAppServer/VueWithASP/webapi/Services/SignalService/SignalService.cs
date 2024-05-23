using Hangfire;
using Nest;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.HangFireResponse;
using webapi.Services.ElasticSearchService;
using webapi.Services.NotificationService;
using webapi.Services.PlatformService;
using webapi.Services.PriceHistoryService;
using webapi.Services.ScraperService;
using webapi.Services.TimmyProductService;
using webapi.Services.UserService;
using webapi.Services.UserSubscriptionProductService;
using webapi.Services.UserSubscriptionService;
using webapi.Utilities.Linq;

namespace webapi.Services.SignalService
{
	public class SignalService : ISignalService
	{
		IElasticSearchService _elasticSearchService;
		IUserSubscriptionProductService _userSubscriptionProductService;
		IUserSubscriptionService _userSubscriptionService;
		ITimmyProductService _timmyProductService;
		IPriceHistoryService _priceHistoryService;
		IPlatformService _platformService;
		IScraperService _scraperService;
		INotificationService _notificationService;
		IUserService _userService;

        public SignalService(IElasticSearchService elasticSearchService, IUserSubscriptionProductService userSubscriptionProductService, 
			IUserSubscriptionService userSubscriptionService, ITimmyProductService timmyProductService, IPriceHistoryService priceHistoryService,
			IPlatformService platformService, IScraperService scraperService, INotificationService notificationService, IUserService userService)
        {
            _elasticSearchService = elasticSearchService;
			_userSubscriptionProductService = userSubscriptionProductService;
			_userSubscriptionService = userSubscriptionService;
			_timmyProductService = timmyProductService;
			_priceHistoryService = priceHistoryService;
			_platformService = platformService;
			_notificationService = notificationService;
			_userService = userService;
			_scraperService = scraperService;
		}

		public async Task<bool> ExecuteScrapeAndGetLowestPrice()
		{
			await ExecuteScrapeCategoryBrandProduct();

			BackgroundJob.Enqueue(() => ExecuteGetWeeklyLowestPrice());

			return true;
		}

		public async Task<bool> ExecuteGetWeeklyLowestPrice()
		{
			//1. Get all adopted timmy product list
			List<TimmyProduct> timmyProducts = await _timmyProductService.GetAllAdoptedProductList();

			//2. For every timmy product get lowest price
			foreach (TimmyProduct timmyProduct in timmyProducts)
			{
				List<ElasticProductDTO> spiderLowestPriceProduct = await _elasticSearchService.GetLowPriceProductForPriceHistory(new ProductSearchTermDTO
				{
					category = timmyProduct.TimmyProductCategory,
					brand = timmyProduct.TimmyProductBrand,
					model = timmyProduct.TimmyProductModel
				});

                if (spiderLowestPriceProduct != null)
				{
					foreach(ElasticProductDTO product in spiderLowestPriceProduct) 
					{
						//3. Add into price history
						bool isAdded = await _priceHistoryService.AddPriceHistory(new AddPriceHistoryDTO
						{
							timmy_product_full_name = timmyProduct.TimmyProductFullName,
							price = product.price_CNY,
							spider = product.spider
						});
					}

				}
			}

			return true;

		}

		public async Task<bool> ExecuteScrapeCategoryBrandProduct()
		{
			bool isScraped  = await _scraperService.ScrapeCategoryBrandProduct();

			return isScraped;
		}

		public async Task<bool> ExecuteScrapeSubscribedProduct(int level)
		{
			try
			{
                await Console.Out.WriteLineAsync("Scrapingggg");
                await _scraperService.ScrapeSubscribedProduct(level);

				return true;
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Dictionary<string,int>> ExecuteSearchBestUserSubscribedProduct(int time)
		{
			// 0. Dictionary to collect how many UserSubscriptionProduct have been updated
			Dictionary<string, int> updatedUserSubscriptionProductDict = new Dictionary<string, int>();

			// 1. get all userSubscription by time
			try 
			{ 
				List<UserSubscription> userSubscriptions = await _userSubscriptionService.GetUserSubscriptionByNotificationTime(time);
				// 2. for loop get product price
				foreach (UserSubscription userSubscription in userSubscriptions)
				{
					int updated = 0;

					await Console.Out.WriteLineAsync($"Getting lowest price for product {userSubscription.UserSubscriptionProductFullName}");

					// 3. Search low price product
					List<ElasticProductDTO> elasticProductDTOs = await _elasticSearchService.GetLowPriceProductForUserSubscribe(userSubscription);

					// 4. filter already added elasticProduct
					// Filter out already added products from elasticProductDTOs
					List<UserSubscriptionProduct> usp = await _userSubscriptionProductService.GetUserSubscriptionProductsByUserSubscriptionId(userSubscription.UserSubscriptionId);

					string[] uniqueIdsArray = usp.Select(u => u.UserSubscriptionProductUniqueId!).ToArray();
					List<ElasticProductDTO> filteredProducts = elasticProductDTOs
						.Where(product => !uniqueIdsArray.Contains(product.unique_id))
						.ToList();

					// Sort filteredProducts by Price_CNY and remove duplicate item
					filteredProducts = filteredProducts
										.OrderBy(product => product.price_CNY)
										.Distinct(new ElasticProductDTOUniqueIdComparer())
										.ToList();

					// 5. add products into UserSubscribedProduct
					foreach (ElasticProductDTO elasticProduct in filteredProducts)
					{
						UserSubscriptionProduct addSubscriptionProduct = await _userSubscriptionProductService.AddUserSubscriptionProduct(new AddUserSubscriptionProductDTO
						{
							userSubscriptionId = userSubscription.UserSubscriptionId,
							productPrice = elasticProduct.price,
							productPriceCNY = elasticProduct.price_CNY,
							productTitle = elasticProduct.title,
							productDescription = elasticProduct.description,
							productCondition = elasticProduct.condition,
							productSpider = elasticProduct.spider,
							productCurrency = elasticProduct.currency,
							productAddedDate = DateTime.Now,
							productUserPreference = 1,
							productURL = elasticProduct.product_url,
							productImage = elasticProduct.product_image,
							productUniqueId = elasticProduct.unique_id
						});

						if (addSubscriptionProduct != null)
						{
							// Got updated product
							updated = 1;

							if (updatedUserSubscriptionProductDict.ContainsKey(userSubscription.UserSubscriptionId))
							{
								// Key exists, set the value to 1
								updatedUserSubscriptionProductDict[userSubscription.UserSubscriptionId] = updatedUserSubscriptionProductDict[userSubscription.UserSubscriptionId] + 1;
							}
							else
							{
								// Key doesn't exist, initialize it with a value of 1
								updatedUserSubscriptionProductDict[userSubscription.UserSubscriptionId] = 1;
							}

						}
					}

					// 5. Send notification to user about better product
					if (updated == 1)
					{

						PublicUserDTO userinfo = await _userService.GetUserInfo(userSubscription.UserId);

						await _notificationService.SendOneMail(new SendMailDTO
						{
							title = "New Updated Product",
							content = "Check out your subscription list for new updated product",
						},
							userinfo.UserEmail!
						);
					}

				}

				return updatedUserSubscriptionProductDict;
			}
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
				
			}

		}

		public async Task<HangFireExecuteUpdateElasticProductDTO> ExecuteUpdateElasticProduct()
		{
			//0. log the update status
			HangFireExecuteUpdateElasticProductDTO hangFireExecuteUpdateElasticProductDTO = new HangFireExecuteUpdateElasticProductDTO();

			//1. get product over 7 days
			List<ElasticProductDTO> productOver7Days = await _elasticSearchService.GetProductOver7Days();

			//2. send url request
			foreach(ElasticProductDTO product in productOver7Days)
			{
				// Delay for 2 second, prevent over request
				await Task.Delay(2000);

				RequestResponseDTO response = await _platformService.SendRequest(product.product_detail_url!);
			
				if(response.responseCode == 200)
				{
					decimal newPrice = _platformService.RetrievePrice(response.responseContentString!, product.spider!);

					//3. Remove product if newPrice == 0
					if(newPrice == 0)
					{
						bool isRemoved = await _elasticSearchService.RemoveOneProduct(product.unique_id!);

						// 3.1. Also remove from the bloom filter
						// TODO not implemented delete the bloom filter yet, add after i finish other things first

						if (isRemoved)
						{
							hangFireExecuteUpdateElasticProductDTO.removedProduct += 1;
						}
					}
					else
					{
						product.price = newPrice;


						product.scraped_date = DateTime.Now;

						//4. update product
						bool isUpdated = await _elasticSearchService.UpdateProduct(product);

						if (isUpdated && newPrice != product.price)
						{
							hangFireExecuteUpdateElasticProductDTO.updatedElasticProductPrice += 1;
						}
					}

				}
				else
				{
					//暂时不处理任何事情，可能可以加入利用ES做的日志
					hangFireExecuteUpdateElasticProductDTO.failedRequest += 1;
				}

			}

            await Console.Out.WriteLineAsync($"Logging info for ExecuteUpdateElasticProduct, Updated price product = {hangFireExecuteUpdateElasticProductDTO.updatedElasticProductPrice}, Failed request count = {hangFireExecuteUpdateElasticProductDTO.failedRequest}");
			return hangFireExecuteUpdateElasticProductDTO;
		}
	}
}
