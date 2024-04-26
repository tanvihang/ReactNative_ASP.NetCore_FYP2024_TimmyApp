namespace webapi.Models.DTO
{
	public class PublicUserDTO
	{
		public string? UserId { get; set; }
		public string? UserName { get; set; }
		public string? UserEmail { get; set; }
		public int? UserLevel { get; set; }
		public DateTime? UserRegisterDate { get; set; }
		public string? UserPhoneNo { get; set; }
	}
}
