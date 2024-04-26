using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models.Response;
using webapi.Services.UserVerificationCodeService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserVerificationCodeController : ControllerBase
	{
        IUserVerificationCodeService _userVerificationCodeService;
        public UserVerificationCodeController(IUserVerificationCodeService userVerificationCodeService)
        {
            _userVerificationCodeService = userVerificationCodeService;
        }

        [HttpGet("SendVerificationCode")]
        public async Task<ResponseData<string>> SendVerificationCode(string email)
        {
            try
            {
				bool valid = await _userVerificationCodeService.GenerateVerificationCode(new Models.DTO.GenerateUserVerificationCodeDTO
				{
					Email = email
				});

                if (valid)
                {
                    return ResponseData<string>.Success($"Email sent to {email}");
                }
                else
                {
					return ResponseData<string>.Failure($"Email format invalid {email}");
				}

			}
            catch (Exception ex)
            {
                return ResponseData<string>.Failure(ex.Message);
            }
            
        }
    }
}
