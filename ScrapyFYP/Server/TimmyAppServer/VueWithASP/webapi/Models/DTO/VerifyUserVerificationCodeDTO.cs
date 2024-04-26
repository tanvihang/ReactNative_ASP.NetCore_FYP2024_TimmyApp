namespace webapi.Models.DTO
{
	public class VerifyUserVerificationCodeDTO
	{
		public string? UserEmail { get; set; }
		public string? VerificationCode { get; set; }
	}
}
