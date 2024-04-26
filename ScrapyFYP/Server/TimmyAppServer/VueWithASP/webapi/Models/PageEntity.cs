namespace webapi.Models
{
	public class PageEntity<T>
	{
		public int Count { get; set; }
		public List<T>? rows { get; set; }
	}
}
