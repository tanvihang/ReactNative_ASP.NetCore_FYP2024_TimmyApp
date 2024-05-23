using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.PriceHistoryService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PriceHistoryController : ControllerBase
	{
        IPriceHistoryService _priceHistoryService;
        public PriceHistoryController(IPriceHistoryService priceHistoryService)
        {
            _priceHistoryService = priceHistoryService;
        }

		[HttpGet("GetProductPriceHistory")]
		public async Task<ResponseData<ResponsePriceHistoryDTO>> GetProductPriceHistory(string category, string brand, string productName)
        {
            try
            {
                string productFullName = category + " " + brand + " " + productName;
                List<PriceHistory> priceHistories = await _priceHistoryService.GetProductPriceHistory(productFullName);

                //Do something to format the data into ResponsePriceHistoryDTO
                ResponsePriceHistoryDTO response = await _priceHistoryService.convertPriceHistoryToResponsePriceHistory(priceHistories, productFullName);
                return ResponseData<ResponsePriceHistoryDTO>.Success(response);
            }
            catch (Exception ex)
            {
                return ResponseData<ResponsePriceHistoryDTO>.Failure(ex.Message);
            }
        }


    }
}
