namespace webapi.Models.DTO
{
	public class AddTimmyProductDTO
	{
		public string category {  get; set; }
		public string brand { get; set; }
		public string model { get; set; }
		public string subModel { get; set; }
		public int adopt { get; set; } = 1;
	}
}
