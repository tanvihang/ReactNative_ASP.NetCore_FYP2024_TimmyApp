using webapi.Models;

namespace webapi.DAO.ScraperDAO
{
	public interface IScraperDAO
	{
		Task<bool> AddScraper(Scraper scraper);
	}
}
