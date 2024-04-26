using System.Collections;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using webapi.Models.BloomFilter;
using webapi.Utilities;

namespace webapi.DAO.BloomFilterDAO
{
	public class BloomFilterDAO : IBloomFilterDAO
	{
		private readonly IConfiguration _configuration;
		private readonly List<string> _spiders;
		private readonly string _baseFilePath;
		private readonly int _bitArraySize;
		private readonly int _numHashes;
		private readonly int _capacity;
		private readonly double _errorRate;
		private Dictionary<string,BloomFilter> _platformBloomFilter;

		public BloomFilterDAO(IConfiguration configuration)
        {
            _configuration = configuration;


			_baseFilePath = _configuration["BloomFilter:FileLocation"]!;
			_capacity = Convert.ToInt32(_configuration["BloomFilter:Capacity"]);
			_errorRate = Convert.ToDouble(_configuration["BloomFilter:Error_Rate"]);
			_bitArraySize = CalculateBitArraySize();
			_numHashes = CalculateNumHashes();

			_spiders = _configuration.GetSection("Spiders").Get<List<string>>()!;

			_platformBloomFilter = new Dictionary<string,BloomFilter>();

			// Create dictionary of string, BloomFilter for every spider
			foreach(string spider in _spiders)
			{
				BloomFilter bloomFilter = new BloomFilter();
				bloomFilter.capacity = _capacity;
				bloomFilter.errorRate = _errorRate;
				bloomFilter.bitArraySize = _bitArraySize;
				bloomFilter.numHashes = _numHashes;
				bloomFilter.bitArray = new sbyte[_bitArraySize];
				bloomFilter.spider = spider;
				bloomFilter.fileUrl = _baseFilePath + spider + "BloomFilter.bin";


				_platformBloomFilter.Add(spider, bloomFilter);
			}

		}

		public async Task<Dictionary<string, BloomFilter>> GetFromFile()
		{
			foreach(string spider in _spiders)
			{

				try
				{
					using (FileStream fs = new FileStream(_platformBloomFilter[spider].fileUrl!, FileMode.Open))
					using (BinaryReader br = new BinaryReader(fs))
					{
						for (int i = 0; i < _platformBloomFilter[spider].bitArray!.Length; i++)
						{
							_platformBloomFilter[spider].bitArray![i] = br.ReadSByte();
						}



						fs.Close();
						br.Close();
					}
				}
				catch(Exception ex)
				{
					throw new Exception(StaticGenerator.GenerateDTOErrorMessage("BloomFilterDAO", "GetFromFile", ex.Message));
				}
				
			}

			return _platformBloomFilter;
		}

		public bool AddProduct(string element, BloomFilter bloomFilter)
		{
			for (int i = 0; i < bloomFilter.numHashes; i++)
			{
				BigInteger hashVal = HashFunction(element, i);
				int index = (int)(hashVal % bloomFilter.bitArraySize);
				bloomFilter.bitArray![index] += 1;
			}

			return true;
		}

		public bool ContainsProduct(string element, BloomFilter bloomFilter)
		{
            Console.WriteLine($"spider: {bloomFilter.spider}");
            Console.WriteLine("Num hashes: "+ bloomFilter.numHashes);
            Console.WriteLine("Bit Array Size: " + bloomFilter.bitArraySize);

            for (int i = 0; i < bloomFilter.numHashes; i++)
			{
				BigInteger hashVal = HashFunction(element, i);
                Console.WriteLine("Calculated hash value: " + hashVal);
                int index = (int)(hashVal % bloomFilter.bitArraySize);
                Console.WriteLine("Responding index: " + index);
                Console.WriteLine("Index value：" + bloomFilter.bitArray[index]);
                if (bloomFilter.bitArray![index] <= 0)
				{
					return false;
				}
			}
			return true;
		}



		public bool RemoveProduct(string element, BloomFilter bloomFilter)
		{
			for (int i = 0; i < bloomFilter.numHashes; i++)
			{
				BigInteger hashVal = HashFunction(element, i);
				int index = (int)(hashVal % bloomFilter.bitArraySize);
				if (bloomFilter.bitArray![i] > 0)
				{
					bloomFilter.bitArray![i] -= 1;
				}
			}

			return true;
		}

		public async Task<bool> SaveToFile(Dictionary<string, BloomFilter> platformBloomFilter)
		{
			foreach(string spider in _spiders)
			{
				using (FileStream fs = new FileStream(platformBloomFilter[spider].fileUrl!, FileMode.Create))
				using (BinaryWriter bw = new BinaryWriter(fs))
				{
					foreach(int val in platformBloomFilter[spider].bitArray!)
					{
						bw.Write(val);
					}
				}
			}

			return true;
		}


		private int CalculateBitArraySize()
		{
			double m = -((double)_capacity * Math.Log(_errorRate)) / (Math.Log(2) * Math.Log(2));
			return (int)Math.Ceiling(m);
		}

		private int CalculateNumHashes()
		{
			double k = ((double)_bitArraySize / _capacity) * Math.Log(2);
			return (int)Math.Ceiling(k);
		}

		BigInteger IBloomFilterDAO.HashFunction(string element, int seed)
		{

            Console.WriteLine(_bitArraySize);

            string input = element + seed.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(input);

			MD5 md5 = MD5.Create();
			byte[] hashBytes = md5.ComputeHash(bytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("x2"));
			}

			string hash = sb.ToString();
			Console.WriteLine(hash);

			BigInteger aa = BigInteger.Parse(hash, System.Globalization.NumberStyles.AllowHexSpecifier);

			if (aa < 0)
			{
				aa += BigInteger.Pow(2, 128);
			}

			return aa;
		}

		BigInteger HashFunction(string element, int seed)
		{
			string input = element + seed.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(input);

			MD5 md5 = MD5.Create();
			byte[] hashBytes = md5.ComputeHash(bytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("x2"));
			}

			string hash = sb.ToString();

			BigInteger aa = BigInteger.Parse(hash, System.Globalization.NumberStyles.AllowHexSpecifier);

			if (aa < 0)
			{
				aa += BigInteger.Pow(2, 128);
			}

			return aa;
		}
	}
}
