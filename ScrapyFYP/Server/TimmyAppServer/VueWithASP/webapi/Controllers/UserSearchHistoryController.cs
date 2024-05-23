using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.Response;
using webapi.Services.JwtService;
using webapi.Services.UserSearchHistory;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserSearchHistoryController : ControllerBase
	{
        private readonly IUserSearchHistoryService  _userSearchHistoryService;
        private readonly IJwtService _jwtService;        

        public UserSearchHistoryController(IUserSearchHistoryService userSearchHistoryService, IJwtService jwtService)
        {
            _userSearchHistoryService = userSearchHistoryService;
            _jwtService = jwtService;
        }

        [HttpPost("GetUserSearchHistory")]
        public async Task<ResponseData<List<UserSearchHistory>>> GetUserSearchHistory(string jwtToken)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);

                List<UserSearchHistory> list = await _userSearchHistoryService.GetUserSearchHistory(userId);

                return ResponseData<List<UserSearchHistory>>.Success(list);
            }
            catch(Exception ex) 
            {
                return ResponseData<List<UserSearchHistory>>.Failure(ex.Message);
            }
        }

        [HttpPost("ClearUserSearchHistory")]
        public async Task<ResponseData<Boolean>> ClearUserSearchHistory(string jwtToken)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);
                Boolean flag = await _userSearchHistoryService.ClearUserSearchHistory(userId);
                return ResponseData<Boolean>.Success(flag);
            }
            catch(Exception ex)
            {
                return ResponseData<Boolean>.Failure(ex.Message);
			}
		}
    }
}
