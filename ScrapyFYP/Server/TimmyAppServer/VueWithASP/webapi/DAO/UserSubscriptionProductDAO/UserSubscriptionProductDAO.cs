using Microsoft.EntityFrameworkCore;
using Nest;
using webapi.Models;
using webapi.Utilities;

namespace webapi.DAO.UserSubscriptionProductDAO
{
	public class UserSubscriptionProductDAO : IUserSubscriptionProductDAO
	{
		TimmyDbContext _context;
        public UserSubscriptionProductDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }

        public async Task<bool> AddUserSubscriptionProduct(UserSubscriptionProduct product)
		{
			try
			{

				UserSubscriptionProduct usp = await _context.UserSubscriptionProducts.FirstOrDefaultAsync(usp => usp.UserSubscriptionProductUniqueId == product.UserSubscriptionProductUniqueId);

				if(usp == null && product != null)
				{
					await _context.UserSubscriptionProducts.AddAsync(product);
					await _context.SaveChangesAsync();

					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionProductDAO", "AddUserSubscriptionProduct", ex.Message));
			}

		}

		public async Task<bool> AddUserSubscriptionProduct2(UserSubscriptionProduct product)
		{

			try
			{
				await _context.UserSubscriptionProducts.AddAsync(product);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionProductDAO", "AddUserSubscriptionProduct2", ex.Message));
			}
		}

		public async Task<int> GetUserSubscriptionProductCount(string userSubscriptionId)
		{
			try
			{
				int count = _context.UserSubscriptionProducts.Where(usp => usp.UserSubscriptionId == userSubscriptionId).Count();
				return count;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionProductDAO", "GetUserSubscriptionProductCount", ex.Message));
			}
		}

		public async Task<List<UserSubscriptionProduct>> GetUserSubscriptionProducts(string userSubscriptionId)
		{
			try
			{
				List<UserSubscriptionProduct> list =  _context.UserSubscriptionProducts.Where(usp => usp.UserSubscriptionId == userSubscriptionId).ToList();
			
				if(list.Count == 0)
				{
					return new List<UserSubscriptionProduct>();
				}

				return list;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionProductDAO", "GetUserSubscriptionProducts", ex.Message));

			}
		}

		public async Task<List<UserSubscriptionProduct>> GetUserSubscriptionProductsByUserSubscriptionId(string userSubscriptionId)
		{
			try
			{
				List<UserSubscriptionProduct> userSubscriptionProductList = await _context.UserSubscriptionProducts.Where(usp => usp.UserSubscriptionId == userSubscriptionId).ToListAsync();
			
				if(userSubscriptionProductList.Count == 0)
				{
					return new List<UserSubscriptionProduct>();
				}

				return userSubscriptionProductList;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscriptionDAO", "GetUserSubscriptionProductsByUserSubscriptionId", ex.Message));
			}
		}

		public async Task<UserSubscriptionProduct> RemoveUserSubscriptionProduct(string userSubscriptionProductId)
		{
			try
			{
				UserSubscriptionProduct product = await _context.UserSubscriptionProducts.FirstOrDefaultAsync(usp => usp.UserSubscriptionProductId == userSubscriptionProductId);

				if(product != null)
				{
					_context.UserSubscriptionProducts.Remove(product);
					await _context.SaveChangesAsync();
					return product;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscribedProductDAO", "RemoveUserSubscriptionProduct", "No product"));
				}
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserSubscribedProductDAO", "RemoveUserSubscriptionProduct", ex.Message));
			}

		}
	}
}
