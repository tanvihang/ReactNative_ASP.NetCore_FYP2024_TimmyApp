using webapi.Models.HangFireResponse;

namespace webapi.Services.SignalService
{
	public interface ISignalService
	{
		Task<bool> ExecuteScrapeSubscribedProduct(int level);
		Task<bool> ExecuteScrapeCategoryBrandProduct();
		Task<Dictionary<string,int>> ExecuteSearchBestUserSubscribedProduct(int notificationTime);
		Task<HangFireExecuteUpdateElasticProductDTO> ExecuteUpdateElasticProduct();
		Task<bool> ExecuteGetWeeklyLowestPrice();
		Task<bool> ExecuteScrapeAndGetLowestPrice();
	}
}
