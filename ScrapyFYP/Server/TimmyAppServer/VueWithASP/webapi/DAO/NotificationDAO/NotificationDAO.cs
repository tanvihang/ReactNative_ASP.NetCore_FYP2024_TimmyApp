using webapi.Models;
using webapi.Utilities;

namespace webapi.DAO.NotificationDAO
{
	public class NotificationDAO : INotificationDAO
	{
		TimmyDbContext _context;
		public NotificationDAO(TimmyDbContext timmyDbContext) { 
			_context = timmyDbContext;
		}

		public async Task<bool> SaveNotification(Notification notification)
		{
			try
			{
				await _context.Notifications.AddAsync(notification);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(StaticGenerator.GenerateDTOErrorMessage("NotificationDTO", "SaveNotification", ex.Message));
			}
		}
	}
}
