using webapi.Models;
using webapi.Utilities;

namespace webapi.DAO.ScraperDAO
{
	public class ScraperDAO : IScraperDAO
	{
		TimmyDbContext _context;
        public ScraperDAO(TimmyDbContext timmyDbContext)
        {
            _context = timmyDbContext;
        }

        public async Task<bool> AddScraper(Scraper scraper)
		{
			try
			{
				await _context.Scrapers.AddAsync(scraper);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("ScraperDAO", "AddScraper", "Failed to save into Scraper table"));
			}
		}
	}
}
