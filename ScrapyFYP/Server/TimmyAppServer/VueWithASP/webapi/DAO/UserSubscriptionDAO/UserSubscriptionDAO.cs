using Microsoft.EntityFrameworkCore;
using Nest;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.DAO.UserSubscriptionDAO
{
	public class UserSubscriptionDAO : IUserSubscriptionDAO
	{
		TimmyDbContext _context;
		public UserSubscriptionDAO(TimmyDbContext timmyDbContext) 
		{
			_context = timmyDbContext;
		}

		public async Task<bool> AddUserSubscription(UserSubscription userSubscription)
		{
			try
			{

				UserSubscription us = await _context.UserSubscriptions.FirstOrDefaultAsync(us =>
						us.UserSubscriptionProductFullName == userSubscription.UserSubscriptionProductFullName &&
						us.UserId == userSubscription.UserId);

				if (us != null) 
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "AddUserSubscription", "Already have same product subscription"));
				}

				await _context.UserSubscriptions.AddAsync(userSubscription);
				await _context.SaveChangesAsync();
				return true;

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "AddUserSubscription", ex.Message));
			}
		}

		public async Task<UserSubscription> GetOneUserSubscription(string userId)
		{
			try
			{
				UserSubscription userSubscription = await _context.UserSubscriptions.FirstOrDefaultAsync(us => us.UserId == userId);

				if (userSubscription != null)
				{
					return userSubscription;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetOneUserSubscription", "User have no subscription"));
				}

			}
			catch 
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetOneUserSubscription", "Error reading UserSSubscription table"));
			}
		}

		public async Task<List<UserSubscription>> GetUserSubscription(string userId)
		{
			try
			{
				List<UserSubscription> userSubscription = await _context.UserSubscriptions.Where(us => us.UserId == userId).ToListAsync();

				if(userSubscription.Count == 0) 
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscription", "User dont have subscription"));
				}

				return userSubscription;

			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscription", ex.Message));
			}
		}

		public async Task<UserSubscription> GetUserSubscriptionBySubscriptionId(string userSubscriptionId)
		{
			try
			{
				UserSubscription userSubscription = await _context.UserSubscriptions.FirstOrDefaultAsync(us => us.UserSubscriptionId == userSubscriptionId);

				return userSubscription;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionBySubscriptionID", ex.Message));
			}

			throw new NotImplementedException();
		}

		public async Task<List<UserSubscription>> GetUserSubscriptionByAboveUserLevel(int level)
		{
			try
			{
				List<UserSubscription> userSubscriptions = await _context.UserSubscriptions.Where(us => us.User.UserLevel >= level).ToListAsync();
			
				if(userSubscriptions.Count == 0)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionByUserLevel", $"No user subscription for level above {level}"));
				}
				
				return userSubscriptions;
			}
			catch(Exception ex)
			{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionByUserLevel", ex.Message));
			}
		}

		public async Task<UserSubscription> RemoveUserSubscription(string userId, string productFullName)
		{
			try
			{
				UserSubscription userSubscription = await _context.UserSubscriptions.FirstOrDefaultAsync(us => us.UserSubscriptionProductFullName == productFullName && us.UserId == userId);

				if(userSubscription == null)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "RemoveUserSubscription", "User Subscription not found"));
				}

				_context.UserSubscriptions.Remove(userSubscription);
				await _context.SaveChangesAsync();
				return userSubscription;

			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "RemoveUserSubscription", ex.Message));
			}
			
		}

		public async Task<List<UserSubscription>> GetUserSubscriptionByNotificationTime(int time)
		{
			try
			{
				List<UserSubscription> userSubscriptions = await _context.UserSubscriptions.Where(us => us.UserSubscriptionNotificationTime == time).ToListAsync();
				
				if(userSubscriptions.Count == 0)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionByNotificationTime", $"No user subscription for time {time}"));
				}

				return userSubscriptions; 
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionByNotificationTime", ex.Message));
			}
		}
	}
}
