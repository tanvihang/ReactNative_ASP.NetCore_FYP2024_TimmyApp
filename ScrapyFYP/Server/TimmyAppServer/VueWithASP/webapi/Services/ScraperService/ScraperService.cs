using Microsoft.Extensions.Configuration;
using Python.Runtime;
using System.Diagnostics;
using webapi.DAO.ScraperDAO;
using webapi.Models.DTO;
using webapi.Utilities;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using Nest;
using webapi.Services.SubscribedProductService;
using webapi.Models;
using webapi.DAO.TimmyProductDAO;
using Hangfire;

namespace webapi.Services.ScraperService
{
	public class ScraperService : IScraperService
	{
		private readonly IConfiguration _configuration;
		private readonly string pythonDLL;
		private readonly string pythonInterpreter;
		private readonly string pythonScript;
		private readonly string workingDirectory;

		private readonly IScraperDAO _scraperDAO;
		private readonly ISubscribedProductService _subscribedProductService;
		private readonly ITimmyProductDAO _timmyProductDAO;

		private readonly string[] spiders = { "mudah", "aihuishou" };

		public ScraperService(IConfiguration configuration, IScraperDAO scraperDAO, ISubscribedProductService subscribedProductService, ITimmyProductDAO timmyProductDAO)
        {
			_configuration = configuration;
			pythonDLL = _configuration["Scrapy:PythonDll"];
			pythonInterpreter = _configuration["Scrapy:PythonInterpreter"];
			pythonScript = _configuration["Scrapy:PythonScript"];
			workingDirectory = _configuration["Scrapy:PythonWorkingDirectory"];
		
			_scraperDAO = scraperDAO;
			_subscribedProductService = subscribedProductService;
			_timmyProductDAO = timmyProductDAO;
			
		}

        public async Task<bool> ScrapeCategoryBrandProduct()
		{

			// 1. 获取CategoryBrand list
			List<CategoryBrandDTO> categoryBrands = await _timmyProductDAO.GetCategoryBrandList();

			// 2. 进行爬取
			foreach (CategoryBrandDTO categoryBrand in categoryBrands)
			{
				ProductScrapeParamsDTO productScrapeParamsDTO = new ProductScrapeParamsDTO
				{
					category = categoryBrand.category,
					brand = categoryBrand.brand,
					model = "",
					spiders = this.spiders,
					isTest = 0,
					iteration = 4
				};

				await this.ScrapeProduct(productScrapeParamsDTO);
				//BackgroundJob.Enqueue(() => ScrapeProduct(productScrapeParamsDTO));
			}

			return true;

		}

		public async Task<bool> ScrapeProduct(ProductScrapeParamsDTO productScrapeParamsDTO)
		{
			ProcessStartInfo startInfo = SetupScript(productScrapeParamsDTO);
			DateTime startTime = DateTime.Now;
			int scrapeCount = 0;
			string pattern = @"scraped: (\d+)";

			// use hangfire to execute this script
			using (Process process = Process.Start(startInfo))
			{
				// Read the output and error streams asynchronously
				process.OutputDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						string data = e.Data.Replace(" ", "");

                        if (e.Data == "success")
						{
							Console.WriteLine("Ran succeed");
						}

						Match match = Regex.Match(e.Data, pattern);

						if (match.Success)
						{
							// Get the captured group value
							string value = match.Groups[1].Value;

							// Convert the value to an integer if needed
							int scrapedValue = int.Parse(value);
							scrapeCount += scrapedValue;
						}

						
                    }
				};

				process.ErrorDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						Console.WriteLine("Error: " + e.Data);
					}
				};

				// Begin asynchronous read operations
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				// Wait for the process to exit
				process.WaitForExit();

				// Output the exit code
				Console.WriteLine("Exit code: " + process.ExitCode);
			}

			try
			{

				bool isScraped = await _scraperDAO.AddScraper(new Models.Scraper
				{
					ScrapeId = StaticGenerator.GenerateId("S_"),
					ScrapeTime = startTime,
					ScrapeProductCount = scrapeCount,
					ScrapeProductCategory = productScrapeParamsDTO.category,
					ScrapeProductBrand = productScrapeParamsDTO.brand,
					ScrapeProductModel = productScrapeParamsDTO.model,
					ScrapeProductIsTest = productScrapeParamsDTO.isTest,
					ScrapeProductIteration = productScrapeParamsDTO.iteration
				});
			}catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("ScraperService", "ScrapeProduct", ex.Message));
			}


			return true;

		}

		public async Task<bool> ScrapeSubscribedProduct(int level)
		{
			 List<SubscribedProduct> productList =  await _subscribedProductService.GetLevelSubscribedProducts(level);

			foreach(SubscribedProduct product in productList)
			{
				ProductScrapeParamsDTO productScrapeParamsDTO = new ProductScrapeParamsDTO
				{
					category = product.SubscribedProductCategory,
					brand = product.SubscribedProductBrand,
					model = product.SubscribedProductModel,
					spiders = this.spiders,
					isTest = 0,
					iteration = 10
				};

				//BackgroundJob.Enqueue<IScraperService>(x => x.ScrapeProduct(productScrapeParamsDTO));

				await this.ScrapeProduct(productScrapeParamsDTO);
			}

			return true;
		}

		private void CreateRunTime()
		{
			Runtime.PythonDLL = this.pythonDLL;
			PythonEngine.Initialize();
		}

		private void CloseRunTime()
		{
			PythonEngine.Shutdown();
		}

		private ProcessStartInfo SetupScript(ProductScrapeParamsDTO productScrapeParamsDTO)
		{
			// Create a StringBuilder to efficiently build the arguments string
			System.Text.StringBuilder argumentsBuilder = new System.Text.StringBuilder();
			argumentsBuilder.Append($"\"{this.pythonScript}\" -c \"{productScrapeParamsDTO.category}\" -b \"{productScrapeParamsDTO.brand}\" -m \"{productScrapeParamsDTO.model}\" -s");

			// Append each spider to the arguments string
			foreach (string spider in productScrapeParamsDTO.spiders)
			{
				argumentsBuilder.Append($" \"{spider}\"");
			}

			argumentsBuilder.Append($" -t {productScrapeParamsDTO.isTest}");
			argumentsBuilder.Append($" -i {productScrapeParamsDTO.iteration}");

			// Convert the StringBuilder to a string
			string arguments = argumentsBuilder.ToString();

			// Create a new process start info
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = this.pythonInterpreter;
			startInfo.Arguments = arguments;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.WorkingDirectory = this.workingDirectory;

			return startInfo;
		}
	}
}
