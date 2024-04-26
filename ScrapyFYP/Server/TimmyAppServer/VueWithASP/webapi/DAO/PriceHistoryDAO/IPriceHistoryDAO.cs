using webapi.Models;

namespace webapi.DAO.PriceHistoryDAO
{
	public interface IPriceHistoryDAO
	{
		Task<bool> AddPriceHistory(PriceHistory priceHistory);
		Task<List<PriceHistory>> GetProductPriceHistory(string timmyProductFullName);
	}
}
