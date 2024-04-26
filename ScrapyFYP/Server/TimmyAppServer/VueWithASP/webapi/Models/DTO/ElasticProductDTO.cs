namespace webapi.Models.DTO
{
	public class ElasticProductDTO
	{
		public string? title { get; set; }
		public decimal? price { get; set; }
		public decimal? price_CNY { get; set; }
		public string? condition { get; set; }
		public string? description { get; set; }
		public string? product_url { get; set; }
		public string? product_detail_url { get; set; }
		public string? product_image {  get; set; }
		public DateTime? created_date { get; set; }
		public DateTime? scraped_date { get; set; }
		public string? country { get; set; }
		public string? state { get; set; }
		public string? currency { get; set; }
		public string? unique_id { get; set; }
		public string? category { get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public string? spider { get; set; }
		public int? is_test { get; set; }
		public string? server {  get; set; }
		public string? root_url { get; set; }

	}
}
