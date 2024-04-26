using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using webapi.Models.BloomFilter;
using webapi.Models.Response;
using webapi.Services.BloomFilterService;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BloomFilterController : ControllerBase
	{
        private readonly IBloomFilterService _bloomFilterService;
        public BloomFilterController(IBloomFilterService bloomFilterService)
        {
            _bloomFilterService = bloomFilterService;
        }

        [HttpPost("ContainsProduct")]
        public async Task<ResponseData<bool>> ContainsProduct(string product, string platform)
        {
            try
            {
                //1. read file
                Dictionary<string,BloomFilter> platformBloomFilter = await _bloomFilterService.GetFromFile();

                bool res = _bloomFilterService.ContainsProduct(product, platformBloomFilter[platform]); 

                return ResponseData<bool>.Success(res);

            }
            catch (Exception ex)
            {
				return ResponseData<bool>.Failure(ex.Message);
			}
		}

        [HttpPost("AddProduct")]
        public async Task<ResponseData<bool>> AddProduct(string product, string platform)
        {
            try
            {
				Dictionary<string, BloomFilter> platformBloomFilter = await _bloomFilterService.GetFromFile();

                bool isAdd = _bloomFilterService.AddProduct(product, platformBloomFilter[platform]);

                await _bloomFilterService.SaveToFile(platformBloomFilter!);
            
                return ResponseData<bool>.Success(isAdd);

			}
            catch(Exception ex)
            {
				return ResponseData<bool>.Failure(ex.Message);

			}

		}

        [HttpGet("CalculateHash")]
        public async Task<ResponseData<string>> Hash(string element, int seed)
        {
            BigInteger has = _bloomFilterService.HashFunction(element, seed);

			return ResponseData<string>.Success(has.ToString());
        }
    }
}
