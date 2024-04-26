namespace webapi.Models.DTO
{
	public class DTOFactory
	{
		public static PublicUserDTO ConvertToPublicUserDTO(UserT user)
		{
			return new PublicUserDTO
			{
				UserId = user.UserId,
				UserEmail = user.UserEmail,
				UserLevel = user.UserLevel,
				UserName = user.UserName,
				UserPhoneNo = user.UserPhoneNo,
				UserRegisterDate = user.UserRegisterDate,
			};
		}
	}
}
