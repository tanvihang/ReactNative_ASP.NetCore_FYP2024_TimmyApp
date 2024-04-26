using webapi.Models.DTO;

namespace webapi.Services.NotificationService
{
	public interface INotificationService
	{
		Task<Boolean> SendOneMail(SendMailDTO sendMailDTO, string userEmail);
	}
}
