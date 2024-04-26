using webapi.Models.DTO;

namespace webapi.Services.ScraperService
{
	public interface IScraperService
	{
		Task<Boolean> ScrapeProduct(ProductScrapeParamsDTO productScrapeParamsDTO);
		Task<Boolean> ScrapeSubscribedProduct(int level);
		Task<Boolean> ScrapeCategoryBrandProduct();

	}
}
