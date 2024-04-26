namespace webapi.Models.ProductData
{
	public class TimmyProductData
	{
		public IDictionary<string, IDictionary<string, IDictionary<string, List<string>>>> Products { get; set; }
		public int Count;

		public TimmyProductData()
		{
			Products = new Dictionary<string, IDictionary<string, IDictionary<string, List<string>>>>();
			Count = 0;
		}

	}
}
