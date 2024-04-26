using webapi.Models;
using webapi.Models.DTO;

namespace webapi.Services.PriceHistoryService
{
	public interface IPriceHistoryService
	{
		Task<bool> AddPriceHistory(AddPriceHistoryDTO addPriceHistoryDTO);
		Task<List<PriceHistory>> GetProductPriceHistory(string timmyProductFullName);
	}
}
