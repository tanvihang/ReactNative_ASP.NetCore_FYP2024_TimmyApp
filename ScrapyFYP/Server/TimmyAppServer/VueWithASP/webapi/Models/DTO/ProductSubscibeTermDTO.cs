namespace webapi.Models.DTO
{
	public class ProductSubscibeTermDTO:ProductSearchTermDTO
	{
		public string? subscription_notification_method {  get; set; }
		public int? subscription_notification_time {  get; set; }
	}
}
