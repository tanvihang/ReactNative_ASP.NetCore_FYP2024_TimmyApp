namespace webapi.Models.DTO
{
	public class ChangeUserInfoDTO
	{
		public string newUserName {  get; set; }
		public string newUserEmail { get; set; }
		public string email { get; set; }
		public string verificationCode {  get; set; }
		public string newPhoneNo {  get; set; }
	}
}
