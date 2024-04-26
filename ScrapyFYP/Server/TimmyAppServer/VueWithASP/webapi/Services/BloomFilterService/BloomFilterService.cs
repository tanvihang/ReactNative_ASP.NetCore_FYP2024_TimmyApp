using System.Numerics;
using webapi.DAO.BloomFilterDAO;
using webapi.Models.BloomFilter;

namespace webapi.Services.BloomFilterService
{
	public class BloomFilterService : IBloomFilterService
	{
		private readonly IBloomFilterDAO _bloomFilterDAO;
        public BloomFilterService(IBloomFilterDAO bloomFilterDAO)
        {
			_bloomFilterDAO = bloomFilterDAO;
        }

		public bool AddProduct(string element, BloomFilter bloomFilter)
		{
			return _bloomFilterDAO.AddProduct(element, bloomFilter);
		}

		public bool ContainsProduct(string element, BloomFilter bloomFilter)
		{
			return _bloomFilterDAO.ContainsProduct(element, bloomFilter);
		}

		public Task<Dictionary<string, BloomFilter>> GetFromFile()
		{
			return _bloomFilterDAO.GetFromFile();
		}

		public BigInteger HashFunction(string element, int seed)
		{
			return _bloomFilterDAO.HashFunction(element, seed);
		}

		public bool RemoveProduct(string element, BloomFilter bloomFilter)
		{
			return _bloomFilterDAO.RemoveProduct(element, bloomFilter);
		}

		public async Task<bool> SaveToFile(Dictionary<string, BloomFilter> platformBloomFilter)
		{
			return await _bloomFilterDAO.SaveToFile(platformBloomFilter);
		}
	}
}
