using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings appsettings;

        public EmailService(IOptions<AppSettings> appsettings)
        {
            this.appsettings = appsettings.Value;
        }

        public async Task SendEmailRecoveryAsync(ApplicationUser usuario, string code_encrypted)
        {
            var Html = string.Format("<h1>DEV4 - OrtoApp</h1>" +
                               "Seu código de Recuperação é:" + $"<h2>{code_encrypted}</h2>");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(usuario.Email));
            email.Subject = "DEV4 - OrtoApp - Codigo de Recuperação - " + usuario.Nome;
            email.Body = new TextPart(TextFormat.Html) { Text = Html };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendEmailVerificationAsync(ApplicationUser usuario, string code_encrypted)
        {
            var Html = string.Format("<h1>DEV4 - OrtoApp</h1>" +
                                            "Seu código de Ativação é: " + $"<h2>{code_encrypted}</h2>");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(usuario.Email));
            email.Subject = "DEV4 - OrtoApp - Código de Ativação - " + usuario.Nome;
            email.Body = new TextPart(TextFormat.Html) { Text = Html };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
