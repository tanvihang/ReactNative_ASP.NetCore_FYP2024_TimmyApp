using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.UserSubscriptionProductService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserSubscriptionProductController : ControllerBase
	{
        private readonly IUserSubscriptionProductService _userSubscriptionProductService;
        public UserSubscriptionProductController(IUserSubscriptionProductService userSubscriptionProductService)
        {
            _userSubscriptionProductService = userSubscriptionProductService;
        }


		[HttpPost("AddUserSubscriptionProduct")]
		public async Task<ResponseData<UserSubscriptionProduct>> AddUserSubscriptionProduct(AddUserSubscriptionProductDTO addUserSubscriptionProductDTO)
		{
			try
			{
				UserSubscriptionProduct isAdded = await _userSubscriptionProductService.AddUserSubscriptionProduct(addUserSubscriptionProductDTO);

				return ResponseData<UserSubscriptionProduct>.Success(isAdded, "User Subscription Product is added");

			}
			catch (Exception ex)
			{
				return ResponseData<UserSubscriptionProduct>.Failure(ex.Message);
			}
		}


		[HttpPost("RemoveUserSubscriptionProduct")]
		public async Task<ResponseData<UserSubscriptionProduct>> RemoveUserSubscriptionProduct(string userSubscriptionProductId)
		{
			try
			{
				UserSubscriptionProduct usp = await _userSubscriptionProductService.RemoveUserSubscriptionProduct(userSubscriptionProductId);

				return ResponseData<UserSubscriptionProduct>.Success(usp, "User Subscription Product is removed");

			}
			catch (Exception ex)
			{
				return ResponseData<UserSubscriptionProduct>.Failure(ex.Message);
			}
		}

		[HttpPost("GetUserSubscriptionProductsByUserSubscriptionId")]
		public async Task<ResponseData<List<UserSubscriptionProduct>>> GetUserSubscriptionProductsByUserSubscriptionId(string userSubscriptionId)
		{
			try
			{
				List<UserSubscriptionProduct> list = await _userSubscriptionProductService.GetUserSubscriptionProductsByUserSubscriptionId(userSubscriptionId);
				return ResponseData<List<UserSubscriptionProduct>>.Success(list);
			}
			catch(Exception ex) 
			{
				return ResponseData<List<UserSubscriptionProduct>>.Failure(ex.Message);
			}
		}
	}
}
