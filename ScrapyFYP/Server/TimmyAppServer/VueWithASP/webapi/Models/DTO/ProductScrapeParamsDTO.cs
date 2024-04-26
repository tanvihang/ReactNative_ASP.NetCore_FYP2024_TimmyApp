namespace webapi.Models.DTO
{
	public class ProductScrapeParamsDTO
	{
		public string? category {  get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public string[]? spiders { get; set; }
		public int? isTest { get; set; }
		public int? iteration { get; set; }
	}
}
