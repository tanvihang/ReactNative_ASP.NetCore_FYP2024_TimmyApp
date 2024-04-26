using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Utilities;

namespace webapi.DAO.PriceHistoryDAO
{
	public class PriceHistoryDAO : IPriceHistoryDAO
	{
		TimmyDbContext _context;
        public PriceHistoryDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }
        public async Task<bool> AddPriceHistory(PriceHistory priceHistory)
		{
			try
			{
				await _context.PriceHistories.AddAsync(priceHistory);
				await _context.SaveChangesAsync();

				return true;
			}
			catch
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("PriceHistoryDAO", "AddPriceHistory", "Failed to add price history"));
			}
		}

		public async Task<List<PriceHistory>> GetProductPriceHistory(string timmyProductFullName)
		{
			try
			{
				List<PriceHistory> list = await _context.PriceHistories.Where(ph => ph.TimmyProductFullName == timmyProductFullName).ToListAsync();

				return list;
			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("PriceHistoryDAO", "GetProductPriceHistory", "Failed to get product price history"));
			}
		}
	}
}
