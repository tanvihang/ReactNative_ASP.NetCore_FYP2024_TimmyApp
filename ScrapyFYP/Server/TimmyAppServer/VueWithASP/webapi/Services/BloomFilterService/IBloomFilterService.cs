using System.Numerics;
using webapi.Models.BloomFilter;

namespace webapi.Services.BloomFilterService
{
	public interface IBloomFilterService
	{
		Task<Dictionary<string, BloomFilter>> GetFromFile();
		Task<bool> SaveToFile(Dictionary<string, BloomFilter> platformBloomFilter);
		bool ContainsProduct(string element, BloomFilter bloomFilter);
		bool RemoveProduct(string element, BloomFilter bloomFilter);
		bool AddProduct(string element, BloomFilter bloomFilter);
		BigInteger HashFunction(string element, int seed);
	}
}
