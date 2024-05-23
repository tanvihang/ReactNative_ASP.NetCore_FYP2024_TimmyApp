using System.Linq;
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
				PriceHistoryPrice = (decimal)addPriceHistoryDTO.price,
				PriceHistoryEffectiveDate = DateTime.Now.Date,
				PriceHistorySpider = addPriceHistoryDTO.spider
			});

			return isAdded;

		}

		public async Task<ResponsePriceHistoryDTO> convertPriceHistoryToResponsePriceHistory(List<PriceHistory> priceHistoryList, string fullName)
		{

			decimal[] aihuishouPrice = priceHistoryList.OrderBy(p => p.PriceHistoryEffectiveDate).Where(p => p.PriceHistorySpider == "aihuishou").Select(p => p.PriceHistoryPrice).ToArray();
			decimal[] mudahPrice = priceHistoryList.OrderBy(p => p.PriceHistoryEffectiveDate).Where(p => p.PriceHistorySpider == "mudah").Select(p=>p.PriceHistoryPrice).ToArray();

			List<decimal[]> prices = new List<decimal[]>();
			prices.Add(aihuishouPrice);
			prices.Add(mudahPrice);

			decimal minPrice = aihuishouPrice[0];

            foreach (decimal[] platformPrice in prices)
            {
                foreach (var price in platformPrice)
                {
                    if(minPrice > price)
					{
						minPrice = price;
					}
                }
            }

            string[] platform = { "aihuishou", "mudah" };
			string product = fullName;

			DateTime[] date = priceHistoryList.OrderBy(p => p.PriceHistoryEffectiveDate).Where(p => p.PriceHistorySpider == "aihuishou").Select(p => p.PriceHistoryEffectiveDate).ToArray();

			Console.WriteLine(date[0]);

            ResponsePriceHistoryDTO responsePriceHistoryDTO = new ResponsePriceHistoryDTO
			{
				platformPrice = prices,
				platform = platform,
				priceDate = date,
				product = product,
				minPrice = minPrice
			};

			return responsePriceHistoryDTO;
		}

		public async Task<List<PriceHistory>> GetProductPriceHistory(string timmyProductFullName)
		{
			List<PriceHistory> priceHistories = await _priceHistoryDAO.GetProductPriceHistory(timmyProductFullName);

			return priceHistories;
		}
	}
}
