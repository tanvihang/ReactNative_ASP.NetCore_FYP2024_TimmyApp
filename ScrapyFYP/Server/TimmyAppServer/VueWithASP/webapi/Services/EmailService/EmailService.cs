using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using webapi.Services.UtilitiesService;

namespace webapi.Services.EmailService
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;
		private readonly IUtilitiesService _utilitiesService;

		public EmailService(IConfiguration configuration, IUtilitiesService utilitiesService)
		{
			_configuration = configuration;
			_utilitiesService = utilitiesService;
		}

		public async Task<string> SendEmail(string email, string subject, string body)
		{
			var smtpServer = _configuration["SMTP:server"];
			int smtpPort = int.Parse(_configuration["SMTP:port"]);
			var senderEmail = _configuration["SMTP:email"];
			var senderPassword = _configuration["SMTP:password"];

			using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
			{
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

				MailMessage message = new MailMessage(senderEmail, email)
				{
					Subject = subject,
					Body = body
				};

				try
				{
					await smtpClient.SendMailAsync(message);
                    await Console.Out.WriteLineAsync("Succeed");
                    return "sent";

				}
				catch (Exception ex)
				{
                    await Console.Out.WriteLineAsync(_utilitiesService.GenerateServiceErrorMessage("EmailService", "SendEmail", ex));
                    return "failed " + ex.Message;
                }
			}
		}

		public async Task<string> SendVerificationCode(string email)
		{
			string verificationCode = Guid.NewGuid().ToString().Substring(0,4);

			string ret = await SendEmail(email, "TimmyApp - Your OneTime Verification Code", verificationCode);

			if(ret == "sent")
			{
				return verificationCode;
			}
			else
			{
				return String.Empty;
			}
		}
	}
}
