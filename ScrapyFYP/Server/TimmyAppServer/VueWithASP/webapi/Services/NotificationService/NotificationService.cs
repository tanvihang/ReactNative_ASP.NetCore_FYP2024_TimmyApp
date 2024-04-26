using System.Net.Mail;
using System.Net;
using webapi.Models.DTO;
using webapi.Utilities;
using webapi.DAO.NotificationDAO;
using webapi.DAO.UserTDAO;

namespace webapi.Services.NotificationService
{
	public class NotificationService : INotificationService
	{
		private readonly IConfiguration _configuration;

		private readonly INotificationDAO _notificationDAO;
		private readonly IUserTDAO _userTDAO;

		public NotificationService(IConfiguration configuration, INotificationDAO notificationDAO, IUserTDAO userTDAO)
        {
            _configuration = configuration;
			_notificationDAO = notificationDAO;
			_userTDAO = userTDAO;
        }
        public async Task<bool> SendOneMail(SendMailDTO sendMailDTO, string userEmail)
		{
			var smtpServer = _configuration["SMTP:server"];
			int smtpPort = int.Parse(_configuration["SMTP:port"]);
			var senderEmail = _configuration["SMTP:email"];
			var senderPassword = _configuration["SMTP:password"];

			// 发送邮箱
			using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
			{
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

				MailMessage message = new MailMessage(senderEmail, userEmail)
				{
					Subject = sendMailDTO.title,
					Body = sendMailDTO.content
				};

				try
				{
					await smtpClient.SendMailAsync(message);

					// get userid if exist
					
					string userId = await _userTDAO.GetUserIdByEmail(userEmail);


					if(userId != null)
					{
						// save notification
						await _notificationDAO.SaveNotification(new Models.Notification
						{
							NotificationId = StaticGenerator.GenerateId("N_"),
							UserId = await _userTDAO.GetUserIdByEmail(userEmail),
							NotificationTitle = sendMailDTO.title,
							NotificationContent = sendMailDTO.content,
							NotificationDate = DateTime.Now,
							NotificationType = "email"
						});
					}
					

					return true;

				}
				catch (Exception ex)
				{
					throw new Exception(StaticGenerator.GenerateServiceErrorMessage("NotificationService", "SendOneEmail", ex.Message));
				}
			}
		}
	}
}
