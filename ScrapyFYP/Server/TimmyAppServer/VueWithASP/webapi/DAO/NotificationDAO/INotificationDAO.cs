using webapi.Models;

namespace webapi.DAO.NotificationDAO
{
	public interface INotificationDAO
	{
		Task<bool> SaveNotification(Notification notification);
	}
}
