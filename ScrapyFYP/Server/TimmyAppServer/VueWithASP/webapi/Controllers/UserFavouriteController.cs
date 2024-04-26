using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.JwtService;
using webapi.Services.UserFavouriteService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserFavouriteController : ControllerBase
	{
        private readonly IUserFavouriteService _userFavouriteService;
        private readonly IJwtService _jwtService;
        public UserFavouriteController(IUserFavouriteService userFavouriteService, IJwtService jwtService)
        {
            _userFavouriteService = userFavouriteService;
            _jwtService = jwtService;
        }

        [HttpPost("FavouriteProduct")]
        public async Task<ResponseData<Boolean>> FavouriteProduct(AddFavouriteProductDTO addFavouriteProductDTO, string jwtToken)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);
				
                Boolean isAdded = await _userFavouriteService.FavouriteProduct(userId, addFavouriteProductDTO.productUniqueId!);

                return ResponseData<Boolean>.Success(isAdded);
            }
            catch (Exception ex)
            {
				return ResponseData<Boolean>.Failure(ex.Message);
			}
		}

		[HttpPost("UnFavouriteProduct")]
		public async Task<ResponseData<UserFavourite>> UnFavouriteProduct(AddFavouriteProductDTO addFavouriteProductDTO, string jwtToken)
		{
			try
			{
				string userId = _jwtService.ParseJwtToUserId(jwtToken);

				UserFavourite userFavourite = await _userFavouriteService.UnFavouriteProduct(userId, addFavouriteProductDTO.productUniqueId!);

				return ResponseData<UserFavourite>.Success(userFavourite);
			}
			catch (Exception ex)
			{
				return ResponseData<UserFavourite>.Failure(ex.Message);
			}
		}


		[HttpPost("GetUserFavourite")]
		public async Task<ResponseData<List<ElasticProductDTO>>> GetUserFavourite(string jwtToken)
		{
			try
			{
				string userId = _jwtService.ParseJwtToUserId(jwtToken);

				List<ElasticProductDTO> elasticProductDTOs = await _userFavouriteService.GetUserFavourite(userId);

				if(elasticProductDTOs.Count == 0)
				{
					return ResponseData<List<ElasticProductDTO>>.Failure($"User {userId} no favourite item");
				}

				return ResponseData<List<ElasticProductDTO>>.Success(elasticProductDTOs);
			}
			catch (Exception ex)
			{
				return ResponseData<List<ElasticProductDTO>>.Failure(ex.Message);
			}
		}
	}
}
