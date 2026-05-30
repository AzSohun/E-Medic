using E_Medic.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace E_Medic.Services
{
    public class EmailService: IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(
                _configuration["SmtpSettings:SenderName"],
                _configuration["SmtpSettings:SenderEmail"]!
                ));

            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
             _configuration["SmtpSettings:Server"]!,
             int.Parse(_configuration["SmtpSettings:Port"] ?? "587"),
             SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _configuration["SmtpSettings:Username"]!,
                _configuration["SmtpSettings:Password"]!);


            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
