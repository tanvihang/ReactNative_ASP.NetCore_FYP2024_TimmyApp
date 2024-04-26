namespace webapi.Models.DTO
{
	public class UserRegisterDTO
	{
		public string UserName { get; set; }
		public string UserPassword { get; set; }
		public string UserEmail { get; set; }
		public string UserPhone { get; set; }

		public string VerificationCode { get; set; }
		
	}
}
