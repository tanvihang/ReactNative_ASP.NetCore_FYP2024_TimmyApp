using webapi.DAO.PriceHistoryDAO;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.Services.PriceHistoryService
{
	public class PriceHistoryService : IPriceHistoryService
	{
		IPriceHistoryDAO _priceHistoryDAO;
        public PriceHistoryService(IPriceHistoryDAO priceHistoryDAO)
        {
            _priceHistoryDAO = priceHistoryDAO;
        }
        public async Task<bool> AddPriceHistory(AddPriceHistoryDTO addPriceHistoryDTO)
		{
			bool isAdded = await _priceHistoryDAO.AddPriceHistory(new PriceHistory
			{
				PriceHistoryId = StaticGenerator.GenerateId("PH_"),
				TimmyProductFullName = addPriceHistoryDTO.timmy_product_full_name,
				PriceHistoryPrice = addPriceHistoryDTO.price,
				PriceHistoryEffectiveDate = DateTime.Now,
				PriceHistorySpider = addPriceHistoryDTO.spider
			});

			return isAdded;

		}

		public async Task<List<PriceHistory>> GetProductPriceHistory(string timmyProductFullName)
		{
			List<PriceHistory> priceHistories = await _priceHistoryDAO.GetProductPriceHistory(timmyProductFullName);

			return priceHistories;
		}
	}
}
