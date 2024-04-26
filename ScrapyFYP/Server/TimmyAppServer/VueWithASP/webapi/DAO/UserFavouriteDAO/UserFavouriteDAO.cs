using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Utilities;

namespace webapi.DAO.UserFavouriteDAO
{
	public class UserFavouriteDAO : IUserFavouriteDAO
	{
		TimmyDbContext _context;

        public UserFavouriteDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }

        public async Task<bool> FavouriteProduct(string userId, string productUniqueId)
		{
			try
			{
				// 1.Check if already favourite
				UserFavourite isFavourited = await _context.UserFavourites.FirstOrDefaultAsync(uf => uf.ProductUniqueId == productUniqueId && uf.UserId == userId);

				if(isFavourited == null)
				{
					await _context.UserFavourites.AddAsync(new UserFavourite
					{
						UserId = userId,
						ProductUniqueId = productUniqueId,
						UserFavouriteDate = DateTime.Now
					});

					await _context.SaveChangesAsync();

					return true;
				}

				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserFavouriteDAO", "FavouriteProduct", $"User {userId} already favourited {productUniqueId}"));

			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserFavouriteDAO", "FavouriteProduct", ex.Message));
			}
		}

		public async Task<List<string>> GetUserFavouriteProductList(string userId)
		{
			try
			{
				List<UserFavourite> userFavouriteList = await _context.UserFavourites.Where(uf => uf.UserId == userId).ToListAsync();

				List<string> productIds = userFavouriteList.Select(uf => uf.ProductUniqueId).ToList();

				return productIds;

			}catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserFavouriteDAO", "GetUserFavouriteProductList", ex.Message));
			}
		}

		public async Task<UserFavourite> UnFavouriteProduct(string userId, string productUniqueId)
		{
			try
			{
				UserFavourite userFavourite = await _context.UserFavourites.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.ProductUniqueId == productUniqueId);
				
				if (userFavourite == null)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserFavouriteDAO", "UnFavouriteProduct", $"Prduct {productUniqueId} not founded for user {userId}"));
				}
				else
				{
					_context.UserFavourites.Remove(userFavourite);
					await _context.SaveChangesAsync();
					return userFavourite;
				}
			
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("UserFavouriteDAO", "UnFavouriteProduct", ex.Message));
			}

		}
	}
}
