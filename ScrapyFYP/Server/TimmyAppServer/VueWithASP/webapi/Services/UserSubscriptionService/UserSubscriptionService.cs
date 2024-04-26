using Microsoft.EntityFrameworkCore;
using webapi.DAO.UserSubscriptionDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Services.SubscribedProductService;
using webapi.Services.TimmyProductService;
using webapi.Services.UserService;
using webapi.Utilities;

namespace webapi.Services.UserSubscriptionService
{
	public class UserSubscriptionService : IUserSubscriptionService
	{
		private readonly IUserSubscriptionDAO _userSubscriptionDAO;
		private readonly ISubscribedProductService _subscribedProductService;
		private readonly IUserService _userService;
		private readonly ITimmyProductService _timmyProductService;

		public UserSubscriptionService(IUserSubscriptionDAO userSubscriptionDAO, ISubscribedProductService subscribedProductService, IUserService userService, ITimmyProductService timmyProductService)
		{
			_userSubscriptionDAO = userSubscriptionDAO;	
			_subscribedProductService = subscribedProductService;
			_userService = userService;
			_timmyProductService = timmyProductService;
		}

		public async Task<UserSubscription> AddUserSubscription(AddUserSubscriptionDTO addUserSubscriptionDTO, string userId)
		{
			// 1. check if TimmyProduct contains the product
			string productFullName = StaticGenerator.GenerateProductFullName(addUserSubscriptionDTO.category!, addUserSubscriptionDTO.brand!, addUserSubscriptionDTO.model!);

			TimmyProduct productExist = await _timmyProductService.GetTimmyProductByName(productFullName);

			if(productExist == null)
			{
				return null;
			}

			string spiders = string.Join(",", addUserSubscriptionDTO.spider!);

			UserSubscription userSubscription = new UserSubscription {
				UserSubscriptionId = StaticGenerator.GenerateId("US_"),
				UserSubscriptionProductFullName = productFullName,
				UserId = userId,
				UserSubscriptionProductCategory = addUserSubscriptionDTO.category,
				UserSubscriptionProductBrand = addUserSubscriptionDTO.brand,
				UserSubscriptionProductModel = addUserSubscriptionDTO.model,
				UserSubscriptionProductSubModel = addUserSubscriptionDTO.brand,
				UserSubscriptionProductDescription = addUserSubscriptionDTO.description,
				UserSubscriptionProductHighestPrice = addUserSubscriptionDTO.highest_price,
				UserSubscriptionProductLowestPrice = addUserSubscriptionDTO.lowest_price,
				UserSubscriptionProductCountry = addUserSubscriptionDTO.country,
				UserSubscriptionProductState = addUserSubscriptionDTO.state,
				UserSubscriptionProductCondition = addUserSubscriptionDTO.condition,
				UserSubscriptionNotificationMethod = addUserSubscriptionDTO.subscription_notification_method,
				UserSubscriptionNotificationTime = addUserSubscriptionDTO.subscription_notification_time,
				UserSubscriptionDate = DateTime.Now,
				UserSubscriptionPrice = 0,
				UserSubscriptionStatus = 0,
				UserSubscriptionSpiders = spiders
			};

			bool isAdded = await _userSubscriptionDAO.AddUserSubscription(userSubscription);

			if (isAdded)
			{
				// 同时加入进入SubscribedProduct
				await _subscribedProductService.AddSubscribedProduct(new UpdateSubscribedProductDTO
				{
					category = addUserSubscriptionDTO.category,
					brand = addUserSubscriptionDTO.brand,
					model = addUserSubscriptionDTO.model,
					user_level = _userService.GetUserInfo(userId).Result.UserLevel
				});
			}


			return userSubscription;
		}

		public async Task<UserSubscription> GetOneUserSubscription(string userId)
		{
			UserSubscription userSubscription = await _userSubscriptionDAO.GetOneUserSubscription(userId);

			return userSubscription;
		}

		public async Task<List<UserSubscription>> GetUserSubscriptionByAboveUserLevel(int level)
		{
			List<UserSubscription> userSubscriptionsList = await _userSubscriptionDAO.GetUserSubscriptionByAboveUserLevel(level);

			return userSubscriptionsList;
		}

		public async Task<List<UserSubscription>> GetUserSubscriptionByNotificationTime(int time)
		{
			List<UserSubscription> userSubscriptionsList = await _userSubscriptionDAO.GetUserSubscriptionByNotificationTime(time);

			return userSubscriptionsList;
		}

		public async Task<UserSubscription> GetUserSubscriptionBySubscriptionId(string userSubscriptionId)
		{
			UserSubscription userSubscription = await _userSubscriptionDAO.GetUserSubscriptionBySubscriptionId(userSubscriptionId);

			return userSubscription;
		}

		public async Task<List<UserSubscription>> GetUserSubscriptions(string userId)
		{
			List<UserSubscription> list = await _userSubscriptionDAO.GetUserSubscription(userId);

			return list;
		}

		public async Task<UserSubscription> RemoveUserSubscription(RemoveUserSubscriptionDTO removeUserSubscriptionDTO, string userId)
		{
			string fullProductName = StaticGenerator.GenerateProductFullName(removeUserSubscriptionDTO.category!, removeUserSubscriptionDTO.brand!, removeUserSubscriptionDTO.model!);
			UserSubscription user = await _userSubscriptionDAO.RemoveUserSubscription(userId, fullProductName);

			PublicUserDTO u = await _userService.GetUserInfo(userId);

			// 同时减少SubscribedProduct数据
			if (user != null)
			{
				await _subscribedProductService.RemoveSubscribedProduct(new UpdateSubscribedProductDTO
				{
					category = removeUserSubscriptionDTO?.category,
					brand = removeUserSubscriptionDTO?.brand,
					model = removeUserSubscriptionDTO?.model,
					user_level = u.UserLevel
				});
			
			}

			return user!;
		}
	}
}
