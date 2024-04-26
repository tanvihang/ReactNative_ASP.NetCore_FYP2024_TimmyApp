using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DAO.SubscribedProductDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.SubscribedProductService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscribedProductController : ControllerBase
	{
        ISubscribedProductService _subscribedProductService;
        public SubscribedProductController(ISubscribedProductService subscribedProductService)
        {
            _subscribedProductService = subscribedProductService;
        }

        [HttpPost("AddSubscribedProduct")]
        public async Task<ResponseData<SubscribedProduct>> AddSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO)
        {
            try
            {
                SubscribedProduct subscribedProduct = await _subscribedProductService.AddSubscribedProduct(updateSubscribedProductDTO);

                return ResponseData<SubscribedProduct>.Success(subscribedProduct);
            }
            catch(Exception ex)
            {
                return ResponseData<SubscribedProduct>.Failure(ex.Message);
            }
        }

		[HttpPost("RemoveSubscribedProduct")]
		public async Task<ResponseData<SubscribedProduct>> RemoveSubscribedProduct(UpdateSubscribedProductDTO updateSubscribedProductDTO)
		{
			try
			{
				SubscribedProduct subscribedProduct = await _subscribedProductService.RemoveSubscribedProduct(updateSubscribedProductDTO);

				return ResponseData<SubscribedProduct>.Success(subscribedProduct);
			}
			catch (Exception ex)
			{
				return ResponseData<SubscribedProduct>.Failure(ex.Message);
			}
		}

		[HttpGet("GetAllSubscribedProduct")]
		public async Task<ResponseData<List<SubscribedProduct>>> GetAllSubscribedProduct()
		{
			try
			{
				List<SubscribedProduct> subscribedProductList = await _subscribedProductService.GetAllSubscribedProduct();

				return ResponseData<List<SubscribedProduct>>.Success(subscribedProductList);
			}
			catch (Exception ex)
			{
				return ResponseData<List<SubscribedProduct>>.Failure(ex.Message);
			}
		}
	}
}
