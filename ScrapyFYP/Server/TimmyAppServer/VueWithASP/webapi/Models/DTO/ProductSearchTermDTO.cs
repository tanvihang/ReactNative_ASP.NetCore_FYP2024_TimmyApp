namespace webapi.Models.DTO
{
	public class ProductSearchTermDTO
	{
		public string? category { get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public string? description { get; set; } = "";
		public decimal? highest_price { get; set; } = decimal.MaxValue;
		public decimal? lowest_price { get; set; } = 0;
		public string? country { get; set; } = string.Empty;
		public string? state { get; set; } = string.Empty;
		public string? condition { get; set; } = string.Empty;
		public string[]? spider { get; set; } = null;
		public string? sort { get; set; } = string.Empty;
		public int? isTest { get; set; } = 0;

		public override string ToString()
		{
			return $"Category: {category}, Brand: {brand}, Model: {model}, Description: {description}, Highest Price: {highest_price}, Lowest Price: {lowest_price}, Country: {country}, State: {state}, Condition: {condition}, Spider: {string.Join(",", spider)}, Sort: {sort}";
		}
	}
}
