using webapi.Models;

namespace webapi.Services.DailyService
{
	public interface IDailySearchService
	{
		Task<DailySearch> SetProduct(string productName);

		Task<Boolean> GetProduct(string productName);

		Task<Boolean> ClearDaily();
	}
}
