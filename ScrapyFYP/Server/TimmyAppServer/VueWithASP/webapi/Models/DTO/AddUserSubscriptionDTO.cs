namespace webapi.Models.DTO
{
	public class AddUserSubscriptionDTO
	{
		public string? subscription_notification_method { get; set; }
		public int? subscription_notification_time { get; set; }
		public string? category { get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public string? description { get; set; }
		public decimal? highest_price { get; set; }
		public decimal? lowest_price { get; set; }
		public string? country { get; set; }
		public string? state { get; set; }
		public string? condition { get; set; }
		public string[]? spider { get; set; }
	}
}
