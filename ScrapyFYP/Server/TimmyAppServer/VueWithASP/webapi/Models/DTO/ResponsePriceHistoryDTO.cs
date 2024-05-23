namespace webapi.Models.DTO
{
	public class ResponsePriceHistoryDTO
	{
		public List<decimal[]> platformPrice { get; set; }
		public string[] platform {  get; set; }
		public DateTime[] priceDate { get; set; }
		public string product {  get; set; }
		public decimal minPrice {  get; set; }
	}
}
