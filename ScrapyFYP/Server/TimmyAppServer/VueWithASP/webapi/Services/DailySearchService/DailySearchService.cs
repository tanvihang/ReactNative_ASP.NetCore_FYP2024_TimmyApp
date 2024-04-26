using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services.DailyService
{
	public class DailySearchService : IDailySearchService
	{

		private readonly TimmyDbContext _context;

		public DailySearchService(TimmyDbContext timmyDbContext) 
		{ 
			_context = timmyDbContext;
		}

		public async Task<Boolean> ClearDaily()
		{
			try
			{
				var allDailySearch = _context.DailySearches.ToList();
				_context.DailySearches.RemoveRange(allDailySearch);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Boolean> GetProduct(string productName)
		{
			try
			{
				DailySearch dailySearch = await _context.DailySearches.FirstOrDefaultAsync(ds => ds.ProductName == productName);
				
				if(dailySearch == null)
				{
					return false;
				}

				return true;

			}catch(Exception ex)
			{
				return false;
			}
		}

		public async Task<DailySearch> SetProduct(string productName)
		{
			try
			{
				DailySearch ds = new DailySearch
				{
					ProductName = productName,
				};
				
				_context.DailySearches.Add(ds);

				await _context.SaveChangesAsync();
				return ds;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
