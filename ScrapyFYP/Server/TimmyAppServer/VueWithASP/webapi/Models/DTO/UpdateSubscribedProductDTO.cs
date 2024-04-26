namespace webapi.Models.DTO
{
	public class UpdateSubscribedProductDTO
	{
		public string? category {  get; set; }
		public string? brand { get; set; }
		public string? model { get; set; }
		public int? user_level { get; set; } = 1;
	}
}
