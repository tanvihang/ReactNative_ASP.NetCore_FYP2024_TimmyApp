using System.Collections;

namespace webapi.Models.BloomFilter
{
	public class BloomFilter
	{
		public int capacity { get; set; }
		public double errorRate {  get; set; } 
		public int bitArraySize { get; set; }
		public int numHashes { get; set; }
		public sbyte[]? bitArray {  get; set; }
		public string? spider {  get; set; }
		public string? fileUrl { get; set; }
	}
}
