using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.ScraperService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScraperController : ControllerBase
	{
        IScraperService _scraperService;
        public ScraperController(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        [HttpPost("ScrapeProduct")]
        public async Task<ResponseData<bool>> ScrapeProduct(ProductScrapeParamsDTO productScrapeParamsDTO)
        {
            try
            {
                BackgroundJob.Enqueue(() => _scraperService.ScrapeProduct(productScrapeParamsDTO));

                return ResponseData<bool>.Success("Added job to hangfire");
            }
            catch (Exception ex)
            {
                return ResponseData<bool>.Failure(ex.Message);
            }


        }

		[HttpPost("ScrapeSubscribedProduct")]
		public async Task<ResponseData<bool>> ScrapeSubscribedProduct(int level)
		{
			try
			{
				BackgroundJob.Enqueue(() => _scraperService.ScrapeSubscribedProduct(level));

				return ResponseData<bool>.Success("Added job to hangfire");
			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

		[HttpGet("ScrapeCategoryBrandProduct")]
		public async Task<ResponseData<bool>> ScrapeCategoryBrandProduct()
		{
			try
			{
				BackgroundJob.Enqueue(() => _scraperService.ScrapeCategoryBrandProduct());

				return ResponseData<bool>.Success("Added job to hangfire");
			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

	}
}
