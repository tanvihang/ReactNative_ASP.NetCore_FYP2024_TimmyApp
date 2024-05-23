using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.JwtService;
using webapi.Services.UserService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
        IUserService _userService;
        IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<ResponseData<string>> Register(UserRegisterDTO userRegisterDTO)
        {
            try
            {
				UserT user = await _userService.Register(userRegisterDTO);

                if(user == null)
                {
					return ResponseData<string>.Failure("Email/Username exist");
				}

				return ResponseData<string>.Success("Register Success");
			}
            catch (Exception ex)
            {
                return ResponseData<string>.Failure(ex.Message);
            }

		}

        [HttpPost("Login")]
        public async Task<ResponseData<string>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                string jwtToken = await _userService.Login(userLoginDTO);
                return ResponseData<string>.Success(jwtToken, "Login Success");
            }
            catch(Exception ex)
            {
                return ResponseData<string>.Failure(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<ResponseData<string>> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                bool isReset = await _userService.ResetPassword(resetPasswordDTO);

                if (isReset)
                {
                    return ResponseData<string>.Success("Password reseted");
                }
                else
                {
                    return ResponseData<string>.Failure("Invalid code");
                }
            }
            catch(Exception ex)
            {
				return ResponseData<string>.Failure(ex.Message);
			}
        }

        [HttpPost("GetUserInfo")]
		public async Task<ResponseData<PublicUserDTO>> GetUserInfo(string jwtToken)
        {
            try
            {
                //1. get id by token
                string userId = _jwtService.ParseJwtToUserId(jwtToken);

                PublicUserDTO user = await _userService.GetUserInfo(userId);


                return ResponseData<PublicUserDTO>.Success(user);
            }
            catch(Exception ex) 
            {
                return ResponseData<PublicUserDTO>.Failure(ex.Message);       
            }
        }
	}
}
