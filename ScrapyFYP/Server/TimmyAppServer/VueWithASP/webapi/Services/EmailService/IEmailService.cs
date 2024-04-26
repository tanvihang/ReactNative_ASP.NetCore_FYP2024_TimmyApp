namespace webapi.Services.EmailService
{
	public interface IEmailService
	{
		// Send email to user
		// Description: This function sends to email.
		// Parameters:
		//   - email: The email address to be sent.
		//   - subject: The email subject.
		//   - body: The email body.
		// Returns:
		//   - string: return the sent body.
		Task<string> SendEmail(string email, string subject, string body);

		// Generate and send email verification code throught SMTP.
		// Description: This function sends verification code to email.
		// Parameters:
		//   - email: The email address to be sent.
		// Returns:
		//   - string: return generated code.
		Task<string> SendVerificationCode(string email);
	}
}
