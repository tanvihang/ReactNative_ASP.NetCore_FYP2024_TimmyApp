namespace webapi.Models.DTO
{
	public class SendMailDTO
	{
		public string title {  get; set; }
		public string content { get; set; }
		public string type { get; set; } = "Email";
	}
}
