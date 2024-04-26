using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.PlatformService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlatformController : ControllerBase
	{
        private readonly IPlatformService _platformService;
        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpPost("SendRequest")]
        public async Task<ResponseData<RequestResponseDTO>> SendRequest(string url)
        {
            try
            {
                RequestResponseDTO response = await _platformService.SendRequest(url);

                return ResponseData<RequestResponseDTO>.Success(response);
            }
            catch (Exception ex)
            {
				return ResponseData<RequestResponseDTO>.Failure(ex.Message);
			}
		}

        [HttpPost("RetrievePrice")]
        public async Task<ResponseData<decimal>> RetrievePrice(string url, string platform)
        {
            try
            {
                RequestResponseDTO response = await _platformService.SendRequest(url);

                await Console.Out.WriteLineAsync(response.responseContentString);

                decimal price = _platformService.RetrievePrice(response.responseContentString!, platform);

                return ResponseData<decimal>.Success(price);
            }
            catch(Exception ex)
            {
				return ResponseData<decimal>.Failure(ex.Message);
			}
		}

	}
}
