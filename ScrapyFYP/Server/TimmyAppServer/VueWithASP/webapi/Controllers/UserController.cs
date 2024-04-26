using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.UserService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<ResponseData<string>> Register(UserLoginDTO userLoginDTO)
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
        public async Task<ResponseData<string>> Register(ResetPasswordDTO resetPasswordDTO)
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
    }
}
