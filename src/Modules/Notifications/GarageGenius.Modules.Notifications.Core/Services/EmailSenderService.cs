using System.Net;
using System.Net.Mail;

namespace GarageGenius.Modules.Notifications.Core.Services;
internal class EmailSenderService : IEmailSenderService
{
	private readonly Serilog.ILogger _logger;

	public EmailSenderService(
		Serilog.ILogger logger)
	{
		_logger = logger;
	}

	public Task SendEmailAsync(string receiver, string subject, string message, byte[]? content = default)
	{
        const string fromMail = "sa.garagegenius@gmail.com";
        const string fromAppPassword = "kltcrhxqrkukmmmh";

        var mail = new MailMessage();
        mail.From = new MailAddress(fromMail);
        mail.Subject = subject;
        mail.Body = $"<html><body>{message}<body><html>";
        mail.IsBodyHtml = true;
        mail.To.Add(new MailAddress(receiver));

        if (content is not null)
        {
	        mail.Attachments.Add(new Attachment(new MemoryStream(content), "reservation.pdf"));
        }

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
	        Port = 587,
	        Credentials = new NetworkCredential(fromMail, fromAppPassword),
	        EnableSsl = true
        };

        smtpClient.Send(mail);

		_logger.Information(
			messageTemplate: "Email sent to {Receiver} with subject {Subject} and message {Message}",
			receiver,
			subject,
			message);

		return Task.CompletedTask;
	}
}
