namespace webapi.Models.DTO
{
	public class AddSubscribedProductDTO
	{
		public string? fullName {  get; set; }
		public string? category {  get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public int? userLevel { get; set; }

	}
}
