namespace webapi.Models.DTO
{
	public class CategoryListAndRespondingBrandListDTO
	{
		public List<string> categories { get; set; }
		public Dictionary<string,List<string>> categoryBrands { get; set; }
	}
}
