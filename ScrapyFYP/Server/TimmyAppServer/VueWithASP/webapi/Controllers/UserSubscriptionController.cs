using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Response;
using webapi.Services.JwtService;
using webapi.Services.UserSubscriptionService;
using webapi.Utilities;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        IUserSubscriptionService _userSubscriptionService;
        IJwtService _jwtService;
        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService, IJwtService jwtService)
        {
            _userSubscriptionService = userSubscriptionService;
            _jwtService = jwtService;
        }

        [HttpPost("AddUserSubscription")]
        public async Task<ResponseData<UserSubscription>> AddUserSubscription(string jwtToken, AddUserSubscriptionDTO addUserSubscriptionDTO)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);

                UserSubscription user = await _userSubscriptionService.AddUserSubscription(addUserSubscriptionDTO, userId);

                if(user == null)
                {
					return ResponseData<UserSubscription>.Failure($"Product not exist in TimmyProduct, cant subscribe: {addUserSubscriptionDTO.model}");

				}

				return ResponseData<UserSubscription>.Success(user, "Successfully added");
            }
            catch (Exception ex)
            {
                return ResponseData<UserSubscription>.Failure(ex.Message);
            }

        }

        [HttpPost("RemoveUserSubscription")]
        public async Task<ResponseData<UserSubscription>> RemoveUserSubscription(RemoveUserSubscriptionDTO removeUserSubscriptionDTO, string jwtToken)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);

                UserSubscription userSubscription = await _userSubscriptionService.RemoveUserSubscription(removeUserSubscriptionDTO, userId);

				return ResponseData<UserSubscription>.Success(userSubscription);
            }
            catch (Exception ex)
            {
                return ResponseData<UserSubscription>.Failure(ex.Message);
            }
        }

        [HttpGet("GetUserSubscriptions")]
        public async Task<ResponseData<List<UserSubscription>>> GetUserSubscriptions(string jwtToken)
        {
            try
            {
                string userId = _jwtService.ParseJwtToUserId(jwtToken);

                List<UserSubscription> userSubscriptions = await _userSubscriptionService.GetUserSubscriptions(userId);

                return ResponseData<List<UserSubscription>>.Success(userSubscriptions, "Got user subscription list");
            }
            catch (Exception ex)
            {
                return ResponseData<List<UserSubscription>>.Failure(ex.Message);
            }
        }

        [HttpPost("GetUserSubscriptionByAboveUserLevel")]
        public async Task<ResponseData<List<UserSubscription>>> GetUserSubscriptionByAboveUserLevel(int level)
        {
            try
            {

                List<UserSubscription> list = await _userSubscriptionService.GetUserSubscriptionByAboveUserLevel(level);
            
                if(list.Count == 0)
                {
					return ResponseData<List<UserSubscription>>.Failure("No user subscription above level " + level);
				}

				return ResponseData<List<UserSubscription>>.Success(list, "Got user subscription list above level " + level);

			}
            catch(Exception ex)
            {
				return ResponseData<List<UserSubscription>>.Failure(ex.Message);

			}

		}

		[HttpPost("GetUserSubscriptionByNotificationTime")]
		public async Task<ResponseData<List<UserSubscription>>> GetUserSubscriptionByNotificationTime(int time)
		{
			try
			{

				List<UserSubscription> list = await _userSubscriptionService.GetUserSubscriptionByNotificationTime(time);

				if (list.Count == 0)
				{
					return ResponseData<List<UserSubscription>>.Failure("No user subscription at time " + time);
				}

				return ResponseData<List<UserSubscription>>.Success(list, "Got user subscription list at time " + time);

			}
			catch (Exception ex)
			{
				return ResponseData<List<UserSubscription>>.Failure(ex.Message);

			}

		}

	    [HttpPost("GetUserSubscriptionBySubscriptionId")]
	    public async Task<ResponseData<UserSubscription>> GetUserSubscriptionBySubscriptionId(string id)
	    {
		    try
		    {

                UserSubscription userSubscription = await _userSubscriptionService.GetUserSubscriptionBySubscriptionId(id);

			    if (userSubscription == null)
			    {
				    return ResponseData<UserSubscription>.Failure("No user subscription id: " + id);
			    }

			    return ResponseData<UserSubscription>.Success(userSubscription, "Got user subscription: " + id);

		    }
		    catch (Exception ex)
		    {
			    return ResponseData<UserSubscription>.Failure(ex.Message);

		    }

	    }
    }
}
