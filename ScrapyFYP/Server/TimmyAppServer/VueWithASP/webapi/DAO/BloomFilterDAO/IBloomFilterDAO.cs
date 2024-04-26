using System.Numerics;
using webapi.Models.BloomFilter;

namespace webapi.DAO.BloomFilterDAO
{
	public interface IBloomFilterDAO
	{
		Task<Dictionary<string,BloomFilter>> GetFromFile();
		Task<bool> SaveToFile(Dictionary<string, BloomFilter> platformBloomFilter);
		bool ContainsProduct(string element, BloomFilter bloomFilter);
		bool RemoveProduct(string element, BloomFilter bloomFilter);
		bool AddProduct(string element, BloomFilter bloomFilter);
		BigInteger HashFunction(string elementm, int seed);
	}
}
