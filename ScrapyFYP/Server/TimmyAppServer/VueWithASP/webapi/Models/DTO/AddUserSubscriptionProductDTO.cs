namespace webapi.Models.DTO
{
	public class AddUserSubscriptionProductDTO
	{
		public string? userSubscriptionId { get; set; }
		public decimal? productPrice { get; set; }
		public decimal? productPriceCNY { get; set; }
		public string? productTitle { get; set; }
		public string? productDescription {  get; set; }
		public string? productCondition { get; set; }
		public string? productSpider { get; set; }
		public string? productCurrency { get; set; }
		public DateTime? productAddedDate { get; set; }
		public int? productUserPreference {  get; set; }
		public string? productURL { get; set; }
		public string? productImage {  get; set; }
		public string? productUniqueId { get; set; }
	}
}
