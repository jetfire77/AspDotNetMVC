using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";  // accesssing EmaiTemplate.cs


        public readonly SMTPConfigModel _smtpConfig;


        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "this is test email from bookwanderer app";
            userEmailOptions.Body = GetEmailBody("TestEmail");

            await SendEmail(userEmailOptions);

        }

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {

                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML

            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential


            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }


        private string GetEmailBody(string templateName)
        {

            var body = File.ReadAllText(string.Format(templatePath, templateName));

            return body;

        }
    }
}
