using Nest;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using webapi.Models.DTO;
using webapi.Utilities;

namespace webapi.Services.PlatformService
{
	public class PlatformService : IPlatformService
	{

		private readonly Dictionary<string, Func<string, decimal>> _getPriceFunction;

		// 创建委托类型，供应RetrievePrice使用
		private delegate Task<decimal> GetPriceDelegate(string content);

		public PlatformService()
		{
			Dictionary<string, Func<string, decimal>> getPriceFunction = new Dictionary<string, Func<string, decimal>>
			{
				{"aihuishou", GetAihuishouPrice},
				{"mudah", GetMudahPrice }
			};

			_getPriceFunction = getPriceFunction;
		}

		public decimal RetrievePrice(string content, string platform)
		{

			// get price
			try
			{

				if (_getPriceFunction.TryGetValue(platform, out Func<string, decimal> priceFunction))
				{
					decimal price = priceFunction(content);
					return price;
				}
				else
				{
					throw new Exception(StaticGenerator.GenerateServiceErrorMessage("PlatformService", "RetrievePrice", $"Platform {platform} not implemented"));
				}
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("PlatformService", "RetrievePrice", ex.Message));
			}
		}

		public async Task<RequestResponseDTO> SendRequest(string url)
		{
			var responseDto = new RequestResponseDTO();
			try
			{
				using (var client = new HttpClient())
				{
					int count = 0;

					// Add headers to the HttpClient instance
					client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36 Edg/123.0.0.0");

					HttpResponseMessage response = await client.GetAsync(url);

					while((int)response.StatusCode != 200 && count < 3)
					{
						await Task.Delay(2000);
						response = await client.GetAsync(url);
						count++;
					}

					responseDto.responseCode = (int)response.StatusCode;
					responseDto.responseContentString = await response.Content.ReadAsStringAsync();
					return responseDto;
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions here
				responseDto.responseCode = 400; // Internal Server Error
				responseDto.responseContentString = ex.Message;
				return responseDto;
			}
		}


		private decimal GetAihuishouPrice(string content)
		{
			try
			{
				//data/goods/price

				dynamic obj = JsonConvert.DeserializeObject(content);

				string code = obj.code;

				if(code == "0")
				{
					string price = obj.data.goods.price;

					decimal priceDecimal = Convert.ToDecimal(price);

					return priceDecimal;
				}
				else
				{
					return 0;
				}

			}
			catch(Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateServiceErrorMessage("PlatformService", "GetAihuishouPrice", ex.Message));
			}


		}

		private decimal GetMudahPrice(string content)
		{
			// \"price\":\"RM 2,900\"

			try
			{
				string pattern = @"\""price\""\s*:\s*\""([^\""]+)\""";

				// Match the pattern in the JSON string
				Match match = Regex.Match(content, pattern);

				// If a match is found, return the price value
				if (match.Success)
				{
					string value = match.Groups[1].Value;

					string pricePattern = @"[^0-9\.]";

					// Replace all matches with an empty string
					string result = Regex.Replace(value, pricePattern, "");


                    return Convert.ToDecimal(result);
				}
				else
				{
					return 0; // No match found
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error extracting price: {ex.Message}");
				return 0;
			}
		}
		}
	}
	
