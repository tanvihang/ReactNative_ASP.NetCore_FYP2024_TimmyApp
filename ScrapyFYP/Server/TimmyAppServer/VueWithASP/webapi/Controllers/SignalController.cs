using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models.Response;
using webapi.Services.SignalService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SignalController : ControllerBase
	{
        ISignalService _signalService;
        public SignalController(ISignalService signalService)
        {
            _signalService = signalService;
        }

        [HttpPost("ExecuteSearchBestUserSubscribedProduct")]
        public async Task<ResponseData<Dictionary<string, int>>> ExecuteSearchBestUserSubscribedProduct(int time)
        {
            try
            {
                Dictionary<string,int> updatedUserSubscribedProduct = await _signalService.ExecuteSearchBestUserSubscribedProduct(time);
            
                if(updatedUserSubscribedProduct.Count == 0)
                {
                    return ResponseData<Dictionary<string, int>>.Failure("No product updated");
                }
                else
                {
                    return ResponseData<Dictionary<string, int>>.Success(updatedUserSubscribedProduct);
                }
            }
            catch(Exception ex)
            {
				return ResponseData<Dictionary<string, int>>.Failure(ex.Message);
			}
        }

        [HttpGet("ExecuteGetWeeklyLowestPrice")]
        public async Task<ResponseData<bool>> ExecuteGetWeeklyLowestPrice()
        {
            try
            {
                bool isTrue = await _signalService.ExecuteGetWeeklyLowestPrice();
            
                return ResponseData<bool>.Success(isTrue);
            }
            catch (Exception ex) 
            {
                return ResponseData<bool>.Failure(ex.Message);
			}
		}

        [HttpGet("ExecuteUpdateElasticProduct")]
        public async Task<ResponseData<bool>> ExecuteUpdateElasticProduct()
        {
			try
			{
				BackgroundJob.Enqueue(() => _signalService.ExecuteUpdateElasticProduct());

				return ResponseData<bool>.Success(true, "Added job to hangfire");

			}
			catch (Exception ex)
			{
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

	}
}
