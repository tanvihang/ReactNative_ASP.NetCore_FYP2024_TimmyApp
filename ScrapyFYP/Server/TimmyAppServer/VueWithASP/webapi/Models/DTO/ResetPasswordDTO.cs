namespace webapi.Models.DTO
{
	public class ResetPasswordDTO
	{
		public string UserEmail { get; set; }
		public string NewPassword { get; set; }
		public string VerificationCode { get; set; }
	}
}
