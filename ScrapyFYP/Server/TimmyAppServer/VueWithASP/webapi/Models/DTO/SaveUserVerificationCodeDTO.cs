namespace webapi.Models.DTO
{
	public class SaveUserVerificationCodeDTO
	{
		public string UserEmail { get; set; }
		public string VerificationCode { get; set; }
		public DateTime ExpireTime { get; set; }
	}
}
